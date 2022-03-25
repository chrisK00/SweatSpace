using SweatSpace.Core.Requests;
using SweatSpace.Tests.Integration.Setup;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SweatSpace.Tests.Integration
{
    public class ExercisesTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _baseHttpClient;

        public ExercisesTests(WebApiFactory factory)
        {
            _baseHttpClient = factory.CreateClient();
        }

        private HttpClient GetExercisesClient(int workoutId)
        {
            return _baseHttpClient.ForController($"{Constants.Workouts}/{workoutId}/{Constants.Exercises}").WithAdminAuth();
        }

        [Fact]
        public async Task AddExerciseToWorkout()
        {
            var request = new AddExerciseRequest { Name = "push ups", Reps = 1, Sets = 1 };
            var response = await GetExercisesClient(WebApiFactory.WorkoutId).PostAsJsonAsync("", request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

    }
}
