using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FixtureController : ControllerBase
{
    private readonly ISMSLogic _businessLogic;

    public FixtureController(ISMSLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllFixtures()
    {
        return Ok(await _businessLogic.GetAllFixtures());
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult> GetFixtureById(int id)
    {
        var fixture = await _businessLogic.GetFixture(id);
        return fixture is null ? NotFound("Cannot find the fixture") : Ok(fixture);
    }

    [HttpPost]
    public async Task<ActionResult> AddFixture([FromBody] AddFixtureDto addFixture)
    {

        var response = await _businessLogic.SaveFixture(addFixture);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();

    }


    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteFixture(int id)
    {
        var response = await _businessLogic.DeleteFixture(id);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();


    }

    [Route("{id}")]
    [HttpPut]
    public async Task<ActionResult> UpdateFixture(int id, [FromBody] Fixture fixture)
    {
        var response = await _businessLogic.AmendFixture(id, fixture);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();
    }

}
