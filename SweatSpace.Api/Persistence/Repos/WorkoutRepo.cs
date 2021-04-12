using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;
using SweatSpace.Persistence.Business;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class WorkoutRepo : IWorkoutRepo
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkoutRepo(DataContext context, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> AddWorkoutAsync(Workout workout)
        {
            await _context.Workouts.AddAsync(workout);
            await _unitOfWork.SaveAllAsync();
            return workout.Id;
        }

        public async Task<WorkoutDto> GetWorkoutDtoAsync(int id)
        {
            return await _context.Workouts.ProjectTo<WorkoutDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(w => w.Id == id);
        }
    }
}