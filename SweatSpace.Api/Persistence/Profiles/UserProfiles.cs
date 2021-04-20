using System.Linq;
using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<UserRegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, MemberDto>().ForMember(dest => dest.Roles, opt => 
            opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));

            CreateMap<Workout, WorkoutDto>();            
        }
    }
}