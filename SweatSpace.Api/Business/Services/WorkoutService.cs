using System.Threading.Tasks;
using AutoMapper;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Business.Services
{
    internal class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepo _workoutRepo;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutService(IWorkoutRepo workoutRepo, IMapper mapper, IUserRepo userRepo, IUnitOfWork unitOfWork)
        {
            _workoutRepo = workoutRepo;
            _mapper = mapper;
            _userRepo = userRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddWorkoutAsync(WorkoutAddDto workoutAddDto, int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            var workout = _mapper.Map<Workout>(workoutAddDto);
            //we got the user from db so he is being tracked by ef
            user.Workouts.Add(workout);
            await _unitOfWork.SaveAllAsync();
            return workout.Id;
        }

        public Task<WorkoutDto> GetWorkoutDtoAsync(int id)
        {
            return _workoutRepo.GetWorkoutDtoAsync(id);
        }
    }
}