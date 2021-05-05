using System.Linq;
using AutoMapper;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<RegisterUserRequest, AppUser>();
            CreateMap<AppUser, MemberResponse>().ForMember(dest => dest.Roles, opt => 
            opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));     
        }
    }
}