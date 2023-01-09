using Microsoft.AspNetCore.Mvc;
using SMS.API.Fake_Database;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FixtureController : ControllerBase
{
    [HttpGet]
    public ActionResult GetAllFixtures()
    {
        return Ok(InMemoryDatabase.Fixtures);
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
        return Ok(InMemoryDatabase.Fixtures);
    }

    [HttpPost]
    public ActionResult AddFixture([FromBody] Fixture fixture)
    {
        fixture.Id = NextIdHelper();
        InMemoryDatabase.Fixtures.Add(fixture);
        return CreatedAtAction(nameof(GetFixtureById), new { id = fixture.Id }, fixture);
    }


    [Route("{id}")]
    [HttpDelete]
    public ActionResult DeleteFixture(int id)
    {
        var fixtureToDelete = InMemoryDatabase.Fixtures.FirstOrDefault(x => x.Id == id);
        if (fixtureToDelete == null)
        {
            return NotFound();
        }
        InMemoryDatabase.Fixtures.Remove(fixtureToDelete);
        return Ok(InMemoryDatabase.Fixtures);
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
        return Ok(InMemoryDatabase.Fixtures);
    }

    private int NextIdHelper()
    {
        var lastId = InMemoryDatabase.Players.Max(x => x.Id);
        return lastId + 1;
    }
}
