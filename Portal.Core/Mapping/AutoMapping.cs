using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Portal.DataLayer.Model.Entities;
using Portal.Infrustructure.ViewModel;

namespace Portal.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping(IConfiguration config)
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>()
                .ForMember(
                    dest=>dest.RoleId,
                    opt=>
                        opt.MapFrom(src=>src.UserRoles.FirstOrDefault().RoleId));
        }
    }
}
