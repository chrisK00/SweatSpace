using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Extensions;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Params;

namespace SweatSpace.Api.Controllers
{
    /// <summary>
    /// Search controller - For the shared exercises
    /// </summary>
    public class FindExercisesController : BaseApiController
    {
        private readonly IExerciseService _exerciseService;
        private readonly IMemoryCache _memoryCache;

        public FindExercisesController(IExerciseService exerciseService, IMemoryCache memoryCache)
        {
            _exerciseService = exerciseService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IEnumerable<Exercise>> GetExercises([FromQuery] ExerciseParams exerciseParams)
        {
            string key = $"exercises-{exerciseParams.PageNumber}-{exerciseParams.ItemsPerPage}-{exerciseParams.Name}";

            if (!_memoryCache.TryGetValue(key, out PagedList<Exercise> exercises))
            {
                exercises = await _exerciseService.FindExercisesAsync(exerciseParams);
                _memoryCache.Set(key, exercises, TimeSpan.FromMinutes(2));
            }

            Response.AddPaginationHeader(exercises.TotalItems, exercises.ItemsPerPage, exercises.PageNumber,
                exercises.TotalPages);
            return exercises;
        }
    }
}