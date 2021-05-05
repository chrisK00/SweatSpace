using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Api.Persistence.Responses;

namespace SweatSpace.Api.Business.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserRequest registerUserRequest);

        /// <summary>
        /// Tries to login a user with the specified username and password
        /// </summary>
        /// <param name="loginUserRequest"></param>
        /// <returns>Token</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        Task<string> LoginAsync(LoginUserRequest loginUserRequest);

        Task<IEnumerable<MemberResponse>> GetMemberResponsesAsync();
        Task EditRolesAsync(int userId, string[] roles);
        Task<IEnumerable<WeeklyStatsUserModel>> GetWeeklyStatsUserModels();

    }
}