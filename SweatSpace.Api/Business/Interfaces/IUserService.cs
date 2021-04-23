using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(UserRegisterDto userRegisterDto);

        /// <summary>
        /// Tries to login a user with the specified username and password
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>UserDto with a token</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task<UserDto> LoginAsync(UserLoginDto userLoginDto);

        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task EditRolesAsync(int userId, string[] roles);
    }
}