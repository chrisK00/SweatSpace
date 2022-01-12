using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Helpers.Profiles;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Interfaces.Services;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using SweatSpace.Core.Services;
using SweatSpace.Core.Exceptions;

namespace SweatSpace.Tests.Services
{
    public class WorkoutServiceTests
    {
        private readonly Mock<IUserRepo> _mockUserRepo;
        private readonly Mock<IWorkoutRepo> _mockWorkoutRepo;
        private readonly Mock<ILogger<WorkoutService>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly IWorkoutService _workoutService;
        private readonly Mock<IWorkoutReadRepo> _mockWorkoutReadRepo;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public WorkoutServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _mockWorkoutRepo = new Mock<IWorkoutRepo>();
            _mockWorkoutReadRepo = new Mock<IWorkoutReadRepo>();
            _mockLogger = new Mock<ILogger<WorkoutService>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WorkoutProfiles>()));
            _workoutService = new WorkoutService(_mockWorkoutRepo.Object, _mapper, _mockUserRepo.Object,
              _mockUnitOfWork.Object, _mockLogger.Object, _mockWorkoutReadRepo.Object);
        }

        [Theory, AutoData]
        public async Task AddWorkout_Returns_NewWorkoutDto(AddWorkoutRequest workoutDto)
        {
            var newWorkoutResponse = await _workoutService.AddWorkoutAsync(workoutDto);

            _mockUnitOfWork.Verify(x => x.SaveAllAsync());

            newWorkoutResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task ToggleLikeWorkout_Throws_KeyNotFound_If_Workout_Doesnt_Exist()
        {
            await _workoutService.Invoking(x => x.ToggleLikeWorkoutAsync(1, 1))
                .Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task CopyWorkout_Makes_A_New_Copied_Workout()
        {
            //arrange
            Workout savedWorkout = null;
            var workoutToCopy = new Workout { Id = 2, Name = "potato", IsCompleted = false };

            _mockWorkoutReadRepo.Setup(x => x.GetWorkoutByIdAsync(workoutToCopy.Id)).ReturnsAsync(workoutToCopy);

            _mockWorkoutRepo.Setup(x => x.AddWorkoutAsync(It.IsAny<Workout>()))
                .Callback<Workout>(x => savedWorkout = x);

            //act
            await _workoutService.CopyWorkoutAsync(workoutToCopy.Id, 2);

            //assert
            _mockUnitOfWork.Verify(x => x.SaveAllAsync());

            savedWorkout.Name.Should().BeEquivalentTo(workoutToCopy.Name);
            savedWorkout.IsCompleted.Should().BeFalse();
            savedWorkout.Id.Should().NotBe(workoutToCopy.Id);
        }


        [Fact]
        public async Task GetWorkoutDto_Returns_Workout_If_Exists()
        {
            //arrange
            var workoutResponse = new WorkoutResponse { Id = 2 };
            _mockWorkoutReadRepo.Setup(x => x.GetWorkoutResponseAsync(workoutResponse.Id)).ReturnsAsync(workoutResponse);

            //act
            var workoutDtoNotNull = await _workoutService.GetWorkoutResponseAsync(workoutResponse.Id);
            var workoutDtoNull = await _workoutService.GetWorkoutResponseAsync(2222);

            //assert
            workoutDtoNotNull.Should().NotBeNull();
            workoutDtoNull.Should().BeNull();
        }

        [Fact]
        public async Task WorkoutCompleted_Throws_When_WorkoutHasNotCompletedAllExercises()
        {
            var workout = new Workout
            {
                Exercises = new List<WorkoutExercise> {
                new WorkoutExercise { IsCompleted = false },
                new WorkoutExercise { IsCompleted = true },
                }
            };

            _mockWorkoutRepo.Setup(_ => _.GetWorkoutByIdAsync(workout.Id)).ReturnsAsync(workout);

            await _workoutService.Invoking(_ => _.WorkoutCompletedAsync(workout.Id))
                .Should().ThrowAsync<AppException>();
        }
    }
}