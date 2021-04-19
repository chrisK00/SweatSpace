using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Controllers
{
    /// <summary>
    /// Search controller - For the shared exercises
    /// </summary>
    public class FindExercisesController : BaseApiController
    {
        private readonly IExerciseService _exerciseService;

        public FindExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IEnumerable<Exercise>> GetExercises([FromQuery]ExerciseParams exerciseParams)
        {
            var exercises = await _exerciseService.FindExercisesAsync(exerciseParams);
            Response.AddPaginationHeader(exercises.TotalItems, exercises.ItemsPerPage, exercises.PageNumber,
                exercises.TotalPages);
            return exercises;
        }
    }
}