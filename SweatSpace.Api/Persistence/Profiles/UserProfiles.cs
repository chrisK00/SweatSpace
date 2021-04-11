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
            CreateMap<AppUser, MemberDto>();
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<Workout, WorkoutDto>();
        }
    }
}
