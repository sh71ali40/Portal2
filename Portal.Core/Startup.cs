using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Portal.Class;
using Portal.DataLayer.Model;
using Portal.Infrustructure;
using Portal.Infrustructure.Class;
using Portal.Infrustructure.Interface;
using Portal.Infrustructure.Service;
using Portal.Mapping;
using Portal.Service.Portal;



namespace Portal
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc().AddPlugins();
            
            services.AddServerSideBlazor();
            
            services.AddKendo();

            services.AddDbContext<PortalContext>((serviceProvider, option) =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("Default"));
            });

            services.AddScoped<IPageModuleService, PageModuleService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IUnitOfWork, PortalContext>();
            services.AddScoped<IErrorLogService, ErrorLogService>();
            services.AddScoped<ICoreController, CoreController>();
            services.AddScoped<IModuleDefService, ModuleDefService>();

            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _configuration["Tokens:Issuer"],
                        ValidAudience = _configuration["Tokens:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"])),
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new AutoMapping(_configuration));
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        context.Items["originalPath"] = context.Request.Path.Value;
            //        context.Request.Path = "/error";
            //        //context.Response.Redirect("/error");
            //    });
            //});
            app.UseExceptionHandler("/error");
            app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.Value.Contains("invalid"))
            //        throw new Exception("Error!");
            //    await next();
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseModuleFinder();

            app.UseEndpoints(endpoints =>
            {
                // Get Route From AppSetting
                var area = _configuration.GetSection("StartRoute:Area").Value;
                var controller = _configuration.GetSection("StartRoute:Controller").Value;
                var action = _configuration.GetSection("StartRoute:Action").Value;

                var controllerEndpoint = "{controller=IsPortalUp}/{action=Index}/{id?}";
                var areaEndPoint = "module-{moduleId}/{area}/{controller=Home}/{action=Index}/{id?}";
                var controllerIsDefault = true;

                // if route data set in app settings
                if (!string.IsNullOrEmpty(controller.Trim()) && !string.IsNullOrEmpty(action.Trim()))
                {
                    if (!string.IsNullOrEmpty(area))
                    {
                        areaEndPoint = $"module-{{moduleId}}/{{area={area}}}/{{controller={controller}}}/{{action={action}}}/{{id?}}";
                        controllerIsDefault = false;
                    }
                    else
                    {
                        controllerEndpoint = $"{{controller={controller}}}/{{action={action}}}/{{id?}}";
                    }
                }


                endpoints.MapControllerRoute(
                    name:  "area",
                    pattern: areaEndPoint);

                // endpoints.MapControllerRoute("area2", "{area}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name:  "controller",
                    pattern: controllerEndpoint);

                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            //app.Use(async (ctx, next) =>
            //{
            //    await next();

            //    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
            //    {
            //        //Re-execute the request so the user gets the error page
                    
            //        ctx.Items["originalPath"] = ctx.Request.Path.Value;
            //        ctx.Request.Path = "/error";
            //        await next();
            //    }
            //});
        }
    }
}
