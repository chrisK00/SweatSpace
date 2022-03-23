using AutoMapper;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;

namespace SweatSpace.Core.Helpers.Profiles
{
    public class ExerciseProfiles : Profile
    {
        public ExerciseProfiles()
        {
            CreateMap<AddExerciseRequest, WorkoutExercise>();
            CreateMap<UpdateExerciseRequest, WorkoutExercise>();

            CreateMap<WorkoutExercise, ExerciseResponse>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Exercise.Name));
        }
    }
}
