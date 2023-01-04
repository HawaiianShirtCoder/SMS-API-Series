using Microsoft.AspNetCore.Mvc;
using SMS.API.Fake_Database;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{



    [HttpGet]
    public ActionResult GetData()
    {
        return Ok(InMemoryDatabase.Players);
    }

    [Route("{id}")]
    [HttpGet]
    public ActionResult GetPlayerById(int id)
    {
        var player = InMemoryDatabase.Players.FirstOrDefault(x => x.Id == id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);

    }

    [HttpPost]
    public ActionResult AddPlayer([FromBody] Player player)
    {
        player.Id = NextIdHelper();
        InMemoryDatabase.Players.Add(player);
        //return Ok(InMemoryDatabase.Players);
        //return NoContent();
        return CreatedAtAction(nameof(GetPlayerById), new { id = player.Id }, player);
    }

    [Route("{id}")]
    [HttpDelete]
    public ActionResult DeletePlayer(int id)
    {
        var playerToDelete = InMemoryDatabase.Players.FirstOrDefault(x => x.Id == id);
        if (playerToDelete == null)
        {
            return NotFound();
        }
        InMemoryDatabase.Players.Remove(playerToDelete);
        return Ok(InMemoryDatabase.Players);
    }

    [Route("{id}")]
    [HttpPut]
    public ActionResult UpdatePlayer(int id, [FromBody] Player player)
    {
        if (id != player.Id)
        {
            return BadRequest("Id mis-match");
        }
        var playerToUpdate = InMemoryDatabase.Players.FirstOrDefault(x => x.Id == id);
        if (playerToUpdate == null)
        {
            return NotFound("Could not find the player to update!");
        }
        var oldId = playerToUpdate.Id;
        InMemoryDatabase.Players.Remove(playerToUpdate);
        player.Id = oldId;
        InMemoryDatabase.Players.Add(player);
        return Ok(InMemoryDatabase.Players);
    }

    private int NextIdHelper()
    {
        var lastId = InMemoryDatabase.Players.Max(x => x.Id);
        return lastId + 1;
    }
}
