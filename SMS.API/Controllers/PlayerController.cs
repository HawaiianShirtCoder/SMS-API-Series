using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    [HttpGet]
    public ActionResult GetData()
    {
        return Ok("Data from the server");
    }
}
