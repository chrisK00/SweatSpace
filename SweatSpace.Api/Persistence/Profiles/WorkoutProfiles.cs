using AutoMapper;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class WorkoutProfiles : Profile
    {
        public WorkoutProfiles()
        {
            CreateMap<AddWorkoutRequest, Workout>();
            CreateMap<UpdateWorkoutRequest, Workout>();
            CreateMap<Workout, WorkoutResponse>();
        }
    }
}