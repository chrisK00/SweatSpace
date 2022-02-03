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
using SweatSpace.Core.Services;
using SweatSpace.Infrastructure.Services;
using Xunit;

namespace SweatSpace.Tests.Unit
{
    public class ExerciseServiceTests
    {
        private readonly Mock<IExerciseRepo> _mockExerciseRepo = new();
        private readonly Mock<IExerciseReadRepo> _mockExerciseReadRepo = new();
        private readonly Mock<IWorkoutRepo> _mockWorkoutRepo = new();
        private readonly Mock<ILogger<ExerciseService>> _mockLogger = new();
        private readonly Mock<IWorkoutReadRepo> _mockWorkoutReadRepo = new();
        private readonly Mock<IUnitOfWork> _mockUnitOfWork = new();

        private readonly IShuffleService _shuffleService = new ShuffleService();
        private readonly IMapper _mapper;
        private readonly IExerciseService _exerciseService;

        public ExerciseServiceTests()
        {
            _mapper = new Mapper(new MapperConfiguration(opt => opt.AddMaps(typeof(WorkoutProfiles).Assembly)));

            _exerciseService = new ExerciseService(_mockWorkoutRepo.Object, _mockUnitOfWork.Object,
                _mapper, _mockExerciseRepo.Object, _shuffleService, _mockLogger.Object,
                _mockExerciseReadRepo.Object, _mockWorkoutReadRepo.Object);
        }

        [Fact]
        public void AddExerciseToWorkout_CreatesNewExercise_IfItDoesntAlreadyExist()
        {
            var request = new AddExerciseRequest { Name = "situps" };
            var workout = new Workout { Id = 1 };
            Exercise newExercise = null;

            _mockWorkoutRepo.Setup(_ => _.GetWorkoutByIdAsync(workout.Id)).ReturnsAsync(workout);
            _mockExerciseRepo.Setup(_ => _.AddExerciseAsync(It.IsAny<Exercise>()))
                .Callback<Exercise>(e => newExercise = e);

            _exerciseService.AddExerciseToWorkoutAsync(request, workout.Id);

            _mockExerciseRepo.Verify(_ => _.AddExerciseAsync(It.IsAny<Exercise>()), Times.Once());
            newExercise.Name.Should().Be(request.Name);
        }

        [Fact]
        public void AddExerciseToWorkout_Adds_ExerciseToWorkout()
        {
            var exercise = new Exercise { Id = 1, Name = "situps" };
            var workout = new Workout { Id = 1 };

            var request = new AddExerciseRequest { Name = exercise.Name };

            _mockWorkoutRepo.Setup(_ => _.GetWorkoutByIdAsync(workout.Id)).ReturnsAsync(workout);
            _mockExerciseRepo.Setup(_ => _.GetExerciseByNameAsync(request.Name)).ReturnsAsync(exercise);

            _exerciseService.AddExerciseToWorkoutAsync(request, workout.Id);

            _mockUnitOfWork.Verify(_ => _.SaveAllAsync(), Times.Once());
            workout.Exercises.Should().Contain(_ => _.Exercise.Id == exercise.Id);
        }

    }
}