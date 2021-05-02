using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Business.Services;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Profiles;
using Xunit;

namespace SweatSpace.Tests.Services
{
    public class WorkoutServiceTests
    {
        private readonly Mock<IUserRepo> _mockUserRepo;
        private readonly Mock<IWorkoutRepo> _mockWorkoutRepo;
        private readonly Mock<ILogger<WorkoutService>> _mockLogger;
        private readonly IMapper _mapper;
        private readonly IWorkoutService _workoutService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public WorkoutServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _mockWorkoutRepo = new Mock<IWorkoutRepo>();
            _mockLogger = new Mock<ILogger<WorkoutService>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WorkoutProfiles>()));
            _workoutService = new WorkoutService(_mockWorkoutRepo.Object, _mapper, _mockUserRepo.Object,
              _mockUnitOfWork.Object, _mockLogger.Object);
        }

        [Theory, AutoData]
        public async Task AddWorkout_Returns_NewWorkoutDto(WorkoutAddDto workoutDto)
        {
            var newWorkoutDto = await _workoutService.AddWorkoutAsync(workoutDto);

            _mockUnitOfWork.Verify(x => x.SaveAllAsync());

            newWorkoutDto.Should().NotBeNull();
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
            var workoutToCopy = new Workout { Id = 2, Name = "potato", IsCompleted = false};

            _mockWorkoutRepo.Setup(x => x.GetWorkoutByIdAsync(workoutToCopy.Id)).ReturnsAsync(workoutToCopy);

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
            var workoutDto = new WorkoutDto { Id = 2 };
            _mockWorkoutRepo.Setup(x => x.GetWorkoutDtoAsync(workoutDto.Id)).ReturnsAsync(workoutDto);

            //act
            var workoutDtoNotNull = await _workoutService.GetWorkoutDtoAsync(workoutDto.Id);
            var workoutDtoNull = await _workoutService.GetWorkoutDtoAsync(2222);

            //assert
            workoutDtoNotNull.Should().NotBeNull();
            workoutDtoNull.Should().BeNull();
        }
    }
}