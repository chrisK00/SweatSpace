﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Business.Exceptions;
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
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IUserRepo _userRepo;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IMapper mapper, SignInManager<AppUser> signInManager, ILogger<UserService> logger,
            ITokenService tokenService, IUserRepo userRepo, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _logger = logger;
            _tokenService = tokenService;
            _userRepo = userRepo;
            _userManager = userManager;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _userRepo.GetMembersAsync();
        }

        public async Task RegisterAsync(UserRegisterDto userRegisterDto)
        {
            await _userRepo.AddUserAsync(_mapper.Map<AppUser>(userRegisterDto), userRegisterDto.Password);
        }

        public async Task<UserDto> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepo.GetUserByNameAsync(userLoginDto.UserName);

            if (user == null)
            {
                _logger.LogError($"{nameof(LoginAsync)} user with the specified username: {userLoginDto.UserName} was not found");
                //we dont want to tell the user if a user with this username actually exists
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            //try and sign-in the user by comparing the passwords
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (!result.Succeeded)
            {
                _logger.LogError($"Failed to login user: Bad credentials");
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = await _tokenService.CreateTokenAsync(user);
            return userDto;
        }

        public async Task EditRolesAsync(int userId, string[] roles)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);
            if (user == null)
            {
                _logger.LogError($"User {userId} could not be found");
                throw new KeyNotFoundException("Could not find user");
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var addToRolesResult = await _userManager.AddToRolesAsync(user, roles.Except(userRoles));

            if (!addToRolesResult.Succeeded)
            {
                _logger.LogError($"Failed to add user: {user.Id} to roles: {JsonSerializer.Serialize(roles)}");
                throw new AppException("Failed to add to roles");
            }

            var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(roles));

            if (!removeFromRolesResult.Succeeded)
            {
                _logger.LogError($"Failed to remove user: {user.Id} from roles: {JsonSerializer.Serialize(userRoles)}");
                throw new AppException("Failed to remove from roles");
            }
        }
    }
}