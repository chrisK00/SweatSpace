﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using SweatSpace.Api.Business.Services;
using SweatSpace.Api.Persistence.Dtos;
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
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public WorkoutServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _mockWorkoutRepo = new Mock<IWorkoutRepo>();
            _mockLogger = new Mock<ILogger<WorkoutService>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WorkoutProfiles>()));
        }

        
        [Fact]
        public async Task GetWorkoutDto_Returns_Workout_If_Exists()
        {
            //arrange
            var workoutDto = new WorkoutDto { Id = 2 };
            _mockWorkoutRepo.Setup(x => x.GetWorkoutDtoAsync(workoutDto.Id)).ReturnsAsync(workoutDto);

            var workoutService = new WorkoutService(_mockWorkoutRepo.Object, _mapper, _mockUserRepo.Object,
               _mockUnitOfWork.Object, _mockLogger.Object);

            //act
            var workoutDtoNotNull = await workoutService.GetWorkoutDtoAsync(workoutDto.Id);
            var workoutDtoNull = await workoutService.GetWorkoutDtoAsync(2222);

            //assert
            Assert.NotNull(workoutDtoNotNull);
            Assert.Null(workoutDtoNull);
        }
    }
}