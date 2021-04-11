using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Interfaces;
using SweatSpace.Api.Persistence.Dtos;
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

        public Task<IEnumerable<MemberDto>> GetMembers()
        {
            return _userRepo.GetMembersAsync();
        }

        public Task Register(UserRegisterDto userRegisterDto)
        {
            return _userRepo.AddUserAsync(_mapper.Map<AppUser>(userRegisterDto), userRegisterDto.Password);
        }

        public async Task<UserDto> Login(UserLoginDto userLoginDto)
        {
            var user = await _userRepo.GetUserByNameAsync(userLoginDto.UserName);

            if (user == null)
            {
                //we dont want to tell the user if a user with this username actually exists
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            //try and sign-in the user by comparing the passwords
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await _tokenService.CreateToken(user);
            return userDto;
        }
    }
}