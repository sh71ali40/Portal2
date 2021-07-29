using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Portal.DataLayer.Model;
using Portal.DataLayer.Model.Entities;
using Portal.Infrustructure.Interface;
using Portal.Infrustructure.ViewModel;


namespace Portal.Infrustructure.Service
{
    public interface IUserService
    {
        UserDto CheckUserNamePassword(string userName, string password);
        UserDto GetUserById(int userId);
        Task<UserDto> GetUserByMobile(string mobile);
        UserDto CurrentUser { get; set; }
        string GenerateJsonWebToken(int userId);
        Task<UserDto> AddNewUser(UserDto user);
        Task<bool> UpdateUser(UserDto user);


    }
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWorkHolder;
        private readonly DbSet<DataLayer.Model.Entities.User> _userDbSet;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserDto CurrentUser { get; set; }
        
        public UserService(IUnitOfWork uow, 
            IPasswordHasher passwordHasher, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _unitOfWorkHolder = uow;
            _userDbSet = uow.Set<User>();
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mapper = mapper;

            var userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value;
            if (userId != null)
            {
                CurrentUser = GetCurrentUserById(int.Parse(userId));
            }
        }

        public UserDto CheckUserNamePassword(string userName, string password)
        {
            
            var dbUser =  _userDbSet.FirstOrDefault(u => u.UserName == userName);

            if (dbUser != null)
            {
                var dbPass = dbUser.UserPass;
                var isPassOk = _passwordHasher.Check(dbPass,password);
                if (isPassOk.Verified)
                {
                    var userDto = _mapper.Map<UserDto>(dbUser);
                    return userDto; 
                }
            }
                
            return null;
        }

        public virtual async Task<UserDto> AddNewUser(UserDto user)
        {
            try
            {
                var dbUser = _mapper.Map<User>(user);
                dbUser.UserRoles.Add(new UserRole {RoleId = user.RoleId});
                _userDbSet.Add(dbUser);
                await _unitOfWorkHolder.SaveAllChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<bool> UpdateUser(UserDto user)
        {
            try
            {
                var dbUser = _mapper.Map<User>(user);
                _userDbSet.Update(dbUser);
                await _unitOfWorkHolder.SaveAllChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GenerateJsonWebToken(int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, userId.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public virtual async Task<UserDto> GetUserByMobile(string mobile)
        {
            var dbUser = await _userDbSet.AsNoTracking().FirstOrDefaultAsync(u => u.Mobile == mobile);
            var userDto = _mapper.Map<UserDto>(dbUser);
            return userDto;
        }
        public UserDto GetUserById(int userId)
        {
            var dbUser =  _userDbSet.FirstOrDefault(u => u.UserId == userId);
            var userDto = _mapper.Map<UserDto>(dbUser);
            return userDto;
        }
        private UserDto GetCurrentUserById(int userId)
        {
            var dbUser = _userDbSet.FirstOrDefault(u => u.UserId == userId);
            var userDto = _mapper.Map<UserDto>(dbUser);
            return dbUser != null ? userDto : null;
        }
    }
}
