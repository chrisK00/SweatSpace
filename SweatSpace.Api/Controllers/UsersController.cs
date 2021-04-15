using Microsoft.AspNetCore.Authorization;

namespace SweatSpace.Api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
    }
}