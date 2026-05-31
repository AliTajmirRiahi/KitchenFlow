using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PresentationControllerBase : ControllerBase
    {

    }
}
