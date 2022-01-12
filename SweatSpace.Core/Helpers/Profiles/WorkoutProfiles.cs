using AutoMapper;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;

namespace SweatSpace.Core.Helpers.Profiles
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