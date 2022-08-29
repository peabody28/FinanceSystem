using Microsoft.AspNetCore.Mvc;

namespace core
{
    [ApiController]
    [Route("[controller]/[action]")]
    public abstract class BaseApiController : ControllerBase
    {

    }
}
