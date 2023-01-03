using Microsoft.AspNetCore.Mvc;
using SMS.Shared.Models;

namespace SMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private List<Player> _players = new List<Player>();

    public PlayerController()
    {
        _players.Add(
            new Player
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Smith",
                Email = "JSmith@web.com",
                PhoneNumber = "123456789",
                IsActivePlayer = true
            });

        _players.Add(new Player
        {
            Id = 2,
            Firstname = "Peter",
            Lastname = "Parker",
            Email = "PParker@web.com",
            PhoneNumber = "999",
            IsActivePlayer = false
        });

        _players.Add(new Player
        {
            Id = 3,
            Firstname = "Ben",
            Lastname = "Stokes",
            Email = "BStokes@web.com",
            PhoneNumber = "999",
            IsActivePlayer = true
        });

    }

    [HttpGet]
    public ActionResult GetData()
    {
        return Ok(_players);
    }

    [Route("{id}")]
    [HttpGet]
    public ActionResult GetPlayerById(int id)
    {
        var player = _players.FirstOrDefault(x => x.Id == id);
        if (player == null)
        {
            return NotFound();
        }
        return Ok(player);

    }
}
