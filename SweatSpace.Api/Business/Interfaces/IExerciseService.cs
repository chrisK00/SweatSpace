using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IExerciseService
    {
        Task AddExerciseToWorkout(ExerciseAddDto exerciseAddDto, int workoutId);       
    }
}