using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Dtos;
using SweatSpace.Api.Persistence.Dtos;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserRequest userRegisterDto);

        /// <summary>
        /// Tries to login a user with the specified username and password
        /// </summary>
        /// <param name="userLoginDto"></param>
        /// <returns>Token</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task<string> LoginAsync(LoginUserRequest userLoginDto);

        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task EditRolesAsync(int userId, string[] roles);
    }
}