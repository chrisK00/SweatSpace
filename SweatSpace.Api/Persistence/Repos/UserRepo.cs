using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Dtos;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class UserRepo : IUserRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepo(UserManager<AppUser> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _userManager.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<AppUser> GetUserByNameAsync(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }

        public async Task AddUserAsync(AppUser user, string password)
        {
            //creates and saves user to db
            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);                   
                }
                throw new ArgumentException(sb.ToString());
            }
        }

        public Task<AppUser> GetUserByIdAsync(int id)
        {
            return _userManager.FindByIdAsync(id.ToString());
        }
    }
}