using Microsoft.AspNetCore.Mvc;
using Moq;
using SMS.API.Controllers;
using SMS.Shared.BLL;
using SMS.Shared.Models;

namespace SMS.Tests.Controllers;

public class PlayerControllerTests
{
    private readonly Mock<ISMSLogic> _mockSmsLogic;
    public PlayerControllerTests()
    {
        _mockSmsLogic = new Mock<ISMSLogic>();
    }

    [Fact]
    public async Task GetPlayerById_SupplyValidId_Return200WithPlayerData()
    {
        //Arrange
        _mockSmsLogic.Setup(x => x.GetPlayer(1)).ReturnsAsync(new Shared.Models.Player
        {
            Id = 1,
            Firstname = "Fred",
            Lastname = "Jones",
            Email = "Fred@jones.com",
            PhoneNumber = "1234567890",
            IsActivePlayer = true,
        }); ;

        var controller = new PlayerController(_mockSmsLogic.Object);

        //Act
        var sut = await controller.GetPlayerById(1);

        //Assert
        Assert.IsType<OkObjectResult>(sut);
        var response = sut as OkObjectResult;
        Assert.IsType<Player>(response.Value);
        var player = response.Value as Player;
        Assert.Equal("Fred", player.Firstname);
    }

    [Fact]
    public async Task GetPlayerById_SupplyInvalidId_Return404NotFound()
    {
        //Arrange
        _mockSmsLogic.Setup(x => x.GetPlayer(1)).ReturnsAsync(new Shared.Models.Player
        {
            Id = 1,
            Firstname = "Fred",
            Lastname = "Jones",
            Email = "Fred@jones.com",
            PhoneNumber = "1234567890",
            IsActivePlayer = true,
        }); ;

        var controller = new PlayerController(_mockSmsLogic.Object);

        //Act
        var sut = await controller.GetPlayerById(2);

        //Assert
        Assert.IsType<NotFoundObjectResult>(sut);
    }
}
