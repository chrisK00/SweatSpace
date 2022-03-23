using AutoMapper;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;
using System.Linq;

namespace SweatSpace.Core.Helpers.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<RegisterUserRequest, AppUser>();
            CreateMap<AppUser, MemberResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));
        }
    }
}