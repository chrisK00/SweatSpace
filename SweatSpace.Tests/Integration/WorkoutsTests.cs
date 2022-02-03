using AutoFixture.Xunit2;
using FluentAssertions;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;
using SweatSpace.Tests.Integration.Setup;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace SweatSpace.Tests.Integration
{
    public class WorkoutsTests : IClassFixture<WebApiFactory>
    {
        private readonly HttpClient _client;

        public WorkoutsTests(WebApiFactory factory)
        {
             _client = factory.CreateClient().ForController("workouts").WithAdminAuth();
        }

        [Theory, AutoData]
        public async Task AddWorkout_Returns_NewWorkout(AddWorkoutRequest request)
        {
            var response = await _client.PostAsJsonAsync("",request);
            var workout = await response.Content.ReadFromJsonAsync<WorkoutResponse>();

            response.StatusCode.Should().Be(HttpStatusCode.Created);
            workout.Should().NotBeNull();
            workout.AppUserId.Should().Be(WebApiFactory.UserId);
            workout.Name.Should().Be(request.Name);
        } 
    }
}
