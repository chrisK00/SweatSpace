using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Api.Persistence.Profiles;
using Xunit;

namespace SweatSpace.Tests.Services
{
    public class WorkoutServiceTests
    {
        private readonly Mock<IUserRepo> _mockUserRepo;
        private readonly Mock<IWorkoutRepo> _mockWorkoutRepo;
        private readonly Mock<ILogger<IWorkoutService>> _mockLogger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        public WorkoutServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepo>();
            _mockWorkoutRepo = new Mock<IWorkoutRepo>();
            _mockLogger = new Mock<ILogger<IWorkoutService>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<WorkoutProfiles>()));
        }

        [Fact]
        public async Task GetWorkoutDtoById_Returns_Workout_If_Exists()
        {
            //arrange
            // _mockUserRepo.Setup().returnsasync
            //new up user service

            //act
            //get workout

            //assert
            //notnull
        }
    }
}