using Microsoft.AspNetCore.Mvc;
using SMS.API.Fake_Database;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.Helper;
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
    public ActionResult GetFixtureById(int id)
    {
        var fixture = InMemoryDatabase.Fixtures.FirstOrDefault(x => x.Id == id);
        if (fixture == null)
        {
            return NotFound("Cannot find the fixture");
        }
        return Ok(fixture);
    }

    [HttpPost]
    public ActionResult AddFixture([FromBody] AddFixtureDto addFixture)
    {
        var fixture = addFixture.ToFixtureModel();
        fixture.Id = NextIdHelper();
        InMemoryDatabase.Fixtures.Add(fixture);
        return CreatedAtAction(nameof(GetFixtureById), new { id = fixture.Id }, fixture);
    }


    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteFixture(int id)
    {
        var wasDeleted = await _businessLogic.DeleteFixture(id);
        return wasDeleted ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError, "Delete failed");



    }

    [Route("{id}")]
    [HttpPut]
    public ActionResult UpdateFixture(int id, [FromBody] Fixture fixture)
    {
        if (id != fixture.Id)
        {
            return BadRequest("Id mis-match");
        }
        var fixtureToUpdate = InMemoryDatabase.Fixtures.FirstOrDefault(x => x.Id == id);
        if (fixtureToUpdate == null)
        {
            return NotFound("Could not find the fixture to update!");
        }
        var oldId = fixtureToUpdate.Id;
        InMemoryDatabase.Fixtures.Remove(fixtureToUpdate);
        fixture.Id = oldId;
        InMemoryDatabase.Fixtures.Add(fixture);
        return Ok(fixture);
    }

    private int NextIdHelper()
    {
        var lastId = InMemoryDatabase.Players.Max(x => x.Id);
        return lastId + 1;
    }
}
