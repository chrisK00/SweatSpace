using Microsoft.AspNetCore.Mvc;

namespace SweatSpace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
