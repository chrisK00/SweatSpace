using AutoMapper;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class ExerciseProfiles : Profile
    {
        public ExerciseProfiles() 
        {
            CreateMap<AddExerciseRequest, WorkoutExercise>();
            CreateMap<UpdateExerciseRequest, WorkoutExercise>();

            CreateMap<WorkoutExercise, ExerciseResponse>().ForMember(d => d.Name, opt =>
            opt.MapFrom(s => s.Exercise.Name));
        }
    }
}
