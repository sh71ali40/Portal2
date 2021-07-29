using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portal.Class;
using Portal.Infrustructure.Interface;


namespace Portal
{
    public static class MvcBuilderPluginExtensions
    {
        private const string PluginsDirectoryName = "Module";
        private static string _pluginsRootPath;
        private static IHostEnvironment _hostingEnvironment;
        private static IMvcBuilder _currentMvcBuilder;
        // private static List<Modules> _modules;

        public static IMvcBuilder AddPlugins(this IMvcBuilder mvc)
        {
            try
            {
                _currentMvcBuilder = mvc;
                var serviceProvider = mvc.Services.BuildServiceProvider();
                var configuration = serviceProvider.GetService<IConfiguration>();
                _hostingEnvironment = serviceProvider.GetService<IHostEnvironment>();
                // _modules = serviceProvider.GetService<IModuleService>().GetAll().ToList();


                _pluginsRootPath = Path.Combine(_hostingEnvironment.ContentRootPath, PluginsDirectoryName);
                ConfigureAssemblyResolve();

                var pluginAssemblies =
                    GetPluginAssemblies()
                        .ToList();


                LoadAssemblies(pluginAssemblies);


                mvc.Services.Configure<RazorViewEngineOptions>(o =>
                {
                    o.AreaViewLocationFormats.Clear();

                    o.AreaViewLocationFormats.Add("/Module/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    o.AreaViewLocationFormats.Add("/Module/{2}/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    //o.AreaViewLocationFormats.Add("/{2}/Views/{1}/{0}" + RazorViewEngine.ViewExtension);
                    //o.AreaViewLocationFormats.Add("/{2}/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
                    //o.AreaViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);


                });
                return mvc;
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        private static void LoadAssemblies(List<Assembly> pluginAssemblies)
        {


            _currentMvcBuilder
                .ConfigureApplicationPartManager(apm =>
                {
                    var mappingConfig = new MapperConfiguration(mc => { });
                    foreach (var pluginAssembly in pluginAssemblies)
                    {
                        if (pluginAssembly.FullName != null && pluginAssembly.FullName.Contains("View"))
                        {
                             
                            
                            var viewAssembly = new  CompiledRazorAssemblyPart(pluginAssembly);
                            
                             apm.ApplicationParts.Add(viewAssembly);
                            continue;
                        }
                        apm.ApplicationParts.Add(new AssemblyPart(pluginAssembly));



                        // run Assemblies startup
                        var startupType = pluginAssembly.GetTypes()
                            .FirstOrDefault(type => typeof(IStartUp).IsAssignableFrom(type));
                        if (startupType == null) continue;
                        var startUp = ActivatorUtilities.CreateInstance(_currentMvcBuilder.Services.BuildServiceProvider(), startupType) as IStartUp;
                        startUp?.ConfigureServices(_currentMvcBuilder.Services);

                        // check controllers have area
                        var controllersType = pluginAssembly.GetTypes()
                            .Where(type => typeof(Microsoft.AspNetCore.Mvc.Controller).IsAssignableFrom(type)
                                           && type.Namespace != "Portal.Module.PageManagement.Controller").ToList();
                        if (controllersType.Any())
                        {
                            foreach (var type in controllersType)
                            {

                                var attrIsDefined = Attribute.IsDefined(type, typeof(AreaAttribute));
                                if (!attrIsDefined)
                                    throw new Exception("Area Is Not Defined For Module");
                            }
                        }



                        // run modules mapper
                        var mapperType = pluginAssembly.GetTypes()
                            .Where(type => typeof(Profile).IsAssignableFrom(type)).ToList();
                        if (mapperType.Any())
                        {
                            mappingConfig = new MapperConfiguration(mc =>
                            {
                                foreach (var type in mapperType)
                                {
                                    var res = ActivatorUtilities.CreateInstance(
                                        _currentMvcBuilder.Services.BuildServiceProvider(), type);
                                    mc.AddProfile(res as Profile);
                                }

                            });

                        }



                    }

                    IMapper mapper = mappingConfig.CreateMapper();
                    _currentMvcBuilder.Services.AddSingleton(mapper);
                });// .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }
        private static void ConfigureAssemblyResolve()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                // Extract dependency name from the full assembly name:
                // FooPlugin.FooClass, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
                var pluginDependencyName = e.Name.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).First() + ".dll";

       
                var pluginDependencyFullName = GetFileList(pluginDependencyName).FirstOrDefault()?.FullName;
                return
                    File.Exists(pluginDependencyFullName)
                        ? Assembly.LoadFile(pluginDependencyFullName)
                        : null;
            };
        }

        private static List<FileInfo> GetFileList(string fileName = "")
        {
            var dir = new DirectoryInfo(_pluginsRootPath);
            var fileList = dir.EnumerateFiles("*.dll", SearchOption.AllDirectories)
                .GroupBy(a => a.Name).SelectMany(a => a.Count() > 1 ? a.Where(d => d.FullName.Contains("bin") && !d.FullName.ToLower().Contains("ref") && d.FullName.ToLower().Contains("debug")) : a)
                .ToList();

            fileList = !string.IsNullOrEmpty(fileName.Trim()) ? fileList.Where(f => f.FullName.Contains(fileName)).ToList() : fileList.Where(f => f.FullName.Contains("Portal.Module")).ToList();

            // .Where(f => _modules.Any(m => f.FullName.Contains(m.Name))).ToList();
            return fileList;
        }
        private static IEnumerable<Assembly> GetPluginAssemblies()
        {

            if (!Directory.Exists(_pluginsRootPath))
            {
                yield break;
            }

            var fileList = GetFileList();

            foreach (var file in fileList)
            {

                yield return Assembly.LoadFile(file.FullName);
            }

        }

    }
}
