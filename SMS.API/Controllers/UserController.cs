using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.User;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    [HttpPost]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        string? token = AuthServices.Login(request.Username, request.Password);
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        return Ok(token);
    }
}
