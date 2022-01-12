using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SweatSpace.Api.Business.Requests;
using SweatSpace.Api.Persistence.Helpers;
using SweatSpace.Core.Requests;
using SweatSpace.Core.Responses;

namespace SweatSpace.Core.Interfaces.Services
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