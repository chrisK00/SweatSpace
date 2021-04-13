using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatSpace.Api.Business.Dtos;

namespace SweatSpace.Api.Controllers
{
   
    [Authorize]
    [Route("workouts/{id}/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {

        [HttpPost]
        public IActionResult AddExercise(ExerciseAddDto exerciseAddDto)
        {
            return Created("Hi", "Hi");
        }

        [HttpPost("exercise-completed")]
        public IActionResult ExerciseCompleted()
        {
            return Created("Hi", "Hi");
        }
    }
}
