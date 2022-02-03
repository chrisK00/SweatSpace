using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.WebUtilities;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;
using SweatSpace.Tests.Integration.Setup;
using System.Collections.Generic;
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

        [Fact]
        public async Task GetWorkouts_WithFilterByMyWorkouts_ReturnsOnlyMyWorkouts()
        {
            var uri = QueryHelpers.AddQueryString(_client.BaseAddress.AbsoluteUri, "filterBy", "myWorkouts");
            var workoutResponses = await _client.GetFromJsonAsync<IEnumerable<WorkoutResponse>>(uri);

            workoutResponses.Should().NotBeEmpty();
            workoutResponses.Should().NotContain(w => w.AppUserId != WebApiFactory.UserId);
        }
    }
}
