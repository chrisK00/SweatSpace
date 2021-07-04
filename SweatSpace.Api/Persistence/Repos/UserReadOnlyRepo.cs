using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Api.Persistence.Responses;
using SweatSpace.Api.Persistence.Entities;
using SweatSpace.Api.Persistence.Interfaces;

namespace SweatSpace.Api.Persistence.Repos
{
    internal class UserReadOnlyRepo : IUserReadOnlyRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserReadOnlyRepo(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync() => 
            await _userManager.Users.ProjectTo<MemberResponse>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
    }
}