using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Business.Services
{
    internal class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IUserRepo _userRepo;

        public UserService(IMapper mapper, SignInManager<AppUser> signInManager,
            ITokenService tokenService, IUserRepo userRepo)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userRepo = userRepo;
        }

        public Task Register(UserRegisterDto userRegisterDto)
        {
           return _userRepo.AddUserAsync(_mapper.Map<AppUser>(userRegisterDto), userRegisterDto.Password);
        }
    }
}