using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SweatSpace.Api.Controllers
{
   
    [Authorize]
    [Route("workouts/{id}/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {

        [HttpPost]
        public IActionResult AddExercise()
        {
            return Created("Hi", "Hi");
        }
    }
}
