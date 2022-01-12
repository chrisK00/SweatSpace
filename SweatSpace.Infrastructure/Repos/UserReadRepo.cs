using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Interfaces.Repos;
using SweatSpace.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SweatSpace.Infrastructure.Repos
{
    internal class UserReadRepo : IUserReadRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserReadRepo(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync() =>
            await _userManager.Users.ProjectTo<MemberResponse>(_mapper.ConfigurationProvider).AsNoTracking().ToListAsync();
    }
}