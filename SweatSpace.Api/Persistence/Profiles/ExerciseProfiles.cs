using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;

namespace SweatSpace.Api.Persistence.Profiles
{
    public class ExerciseProfiles : Profile
    {
        public ExerciseProfiles() 
        {
            CreateMap<AddExerciseRequest, WorkoutExercise>();
            CreateMap<UpdateExerciseRequest, WorkoutExercise>();

            CreateMap<WorkoutExercise, ExerciseDto>().ForMember(d => d.Name, opt =>
            opt.MapFrom(s => s.Exercise.Name));
        }
    }
}
