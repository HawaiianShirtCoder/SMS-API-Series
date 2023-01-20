using Microsoft.AspNetCore.Mvc;
using Moq;
using SMS.API.Controllers;
using SMS.Shared.BLL;
using SMS.Shared.Models;

namespace SMS.Tests.Controllers;

public class PlayerControllerTests
{
    [Fact]
    public async Task GetPlayerById_SupplyValidId_Returns200WithPlayerData()
    {
        //Arrange
        var mockLogic = new Mock<ISMSLogic>();
        mockLogic.Setup(x => x.GetPlayer(1))
            .ReturnsAsync(new Shared.Models.Player { Id = 1, Firstname = "Test", Lastname = "Method" });

        var controller = new PlayerController(mockLogic.Object);

        //Act
        var sut = await controller.GetPlayerById(1);

        //Assert
        Assert.IsType<OkObjectResult>(sut);
        var response = sut as OkObjectResult;
        Assert.IsType<Player>(response.Value);
        var player = response.Value as Player;
        Assert.Equal("Test", player?.Firstname);
    }
}
