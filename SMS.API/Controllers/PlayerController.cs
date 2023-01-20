using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ISMSLogic _businessLogic;

    public PlayerController(ISMSLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    [HttpGet]
    public async Task<ActionResult> GetPlayersSummary()
    {

        return Ok(await _businessLogic.GetPlayersSummary());

    }

    [Route("{id}")]
    [HttpGet]
    public async Task<ActionResult> GetPlayerById(int id)
    {
        var player = await _businessLogic.GetPlayer(id);
        return player is null ? NotFound("Cannot find the player") : Ok(player);

    }

    //[Route("PlayerStatus/{isActivePlayer:bool}")]
    [Route("{isActivePlayer:bool}")]
    [HttpGet]
    public async Task<ActionResult> GetPlayersByStatus(bool isActivePlayer)
    {
        return Ok(await _businessLogic.GetPlayersByStatus(isActivePlayer));
    }

    [HttpPost]
    public async Task<ActionResult> AddPlayer([FromBody] AddPlayerDto addPlayer)
    {
        var response = await _businessLogic.SavePlayer(addPlayer);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeletePlayer(int id)
    {
        var response = await _businessLogic.DeletePlayer(id);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();
    }

    [Route("{id}")]
    [HttpPut]
    public async Task<ActionResult> UpdatePlayer(int id, [FromBody] Player player)
    {
        var response = await _businessLogic.AmendPlayer(id, player);
        if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
        {
            return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
        }
        return NoContent();
    }

}
