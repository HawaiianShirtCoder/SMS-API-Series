using Microsoft.AspNetCore.Mvc;
using SMS.API.Fake_Database;
using SMS.Shared.DAL;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.Helper;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FixtureController : ControllerBase
{
    private readonly IDataAccess _dataAccess;
    private readonly IConfiguration _config;
    private readonly string _connectionString = string.Empty;

    public FixtureController(IDataAccess dataAccess, IConfiguration config)
    {
        _dataAccess = dataAccess;
        _config = config;
        _connectionString = _config.GetConnectionString("SMS")!;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllFixtures()
    {
        //var fixtureSummary = (from f in InMemoryDatabase.Fixtures
        //                      select new FixtureSummaryDto
        //                      {
        //                          Id = f.Id,
        //                          Opponent = f.Opponent,
        //                          DateOfFixture = f.DateOfFixture,
        //                          StartTime = f.StartTime,
        //                          Venue = f.Venue.ToString(),
        //                          NumberOfPlayersRequired = f.NumberOfPlayersRequired,
        //                      }).ToList();
        //return Ok(fixtureSummary);

        var sqlStatement = "SELECT * FROM Fixture;";
        var query = await _dataAccess.RunAQuery<Fixture, dynamic>(sqlStatement, new { }, _connectionString);
        var fixtureSummary = (from q in query
                              select new FixtureSummaryDto
                              {
                                  Id = q.Id,
                                  Opponent = q.Opponent,
                                  DateOfFixture = q.DateOfFixture,
                                  NumberOfPlayersRequired = q.NumberOfPlayersRequired,
                                  StartTime = q.StartTime,
                                  Venue = q.Venue.ToString()
                              }).ToList();
        return Ok(fixtureSummary);
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
        //var fixtureToDelete = InMemoryDatabase.Fixtures.FirstOrDefault(x => x.Id == id);
        //if (fixtureToDelete == null)
        //{
        //    return NotFound();
        //}
        //InMemoryDatabase.Fixtures.Remove(fixtureToDelete);
        //return NoContent();
        var sqlStatement = "DELETE FROM Fixture WHERE id = @id";
        try
        {
            await _dataAccess.ExecuteACommand(sqlStatement, new { id = id }, _connectionString);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Logging 
            return NotFound();
        }


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
