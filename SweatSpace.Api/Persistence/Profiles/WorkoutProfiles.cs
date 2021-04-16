using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class WorkoutProfiles : Profile
    {
        public WorkoutProfiles()
        {
            CreateMap<WorkoutAddDto, Workout>();
            CreateMap<WorkoutUpdateDto, Workout>();
        }
    }
}