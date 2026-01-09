using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ISMSLogic _businessLogic;

    public EventsController(ISMSLogic bol)
    {
        _businessLogic = bol;
    }
    [HttpGet]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _businessLogic.GetAllEvents();
        return Ok(events);
    }
}
