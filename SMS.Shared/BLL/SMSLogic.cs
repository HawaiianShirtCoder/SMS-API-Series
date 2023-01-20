using Microsoft.Extensions.Configuration;
using SMS.Shared.DAL;
using SMS.Shared.DTOs;
using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

namespace SMS.Shared.BLL;

public class SMSLogic : ISMSLogic
{
    private readonly IDataAccess _dataAccess;
    private readonly IConfiguration _config;
    private readonly string _connectionString = string.Empty;

    public SMSLogic(IDataAccess dataAccess, IConfiguration config)
    {
        _dataAccess = dataAccess;
        _config = config;
        _connectionString = _config.GetConnectionString("SMS")!;
    }

    #region Players logic


    public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary()
    {
        // pre processing 
        var sqlStatement = "select * from [dbo].[Player];";
        var players = await _dataAccess.RunAQuery<Player, dynamic>(
            sqlStatement,
            new { },
            _connectionString);
        // post processing logic - mapping from player objects to PlayerSummaryDto logics
        var summaries = (from p in players
                         select new PlayerSummaryDto
                         {
                             Id = p.Id,
                             Fullname = $"{p.Firstname} {p.Lastname}",
                             IsActivePlayer = p.IsActivePlayer
                         }).ToList();
        return summaries;
    }

    public async Task<Player?> GetPlayer(int id)
    {
        var sqlStatement = "select * from [dbo].[Player] WHERE id = @id;";
        var player = await _dataAccess.RunAQuery<Player, dynamic>(
            sqlStatement,
            new { id },
            _connectionString);
        return player.FirstOrDefault();
    }

    public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActivePlayer)
    {
        var summary = await GetPlayersSummary();
        return summary.Where(p => p.IsActivePlayer == isActivePlayer).ToList();
    }

    public async Task<ExecuteCommandResponseDto> SavePlayer(AddPlayerDto playerToSave)
    {
        // pre - processing example
        // map addPlayerDto to player
        var response = new ExecuteCommandResponseDto();
        var player = new Player
        {
            Email = playerToSave.Email,
            Firstname = playerToSave.Firstname,
            Lastname = playerToSave.Lastname,
            PhoneNumber = playerToSave.PhoneNumber,
            IsActivePlayer = playerToSave.IsActivePlayer
        };

        var sqlStatement =
            @"insert INTO [dbo].[Player]
                ([FirstName]
                ,[Lastname]
                ,[Email]
                ,[PhoneNumber]
                ,[IsActivePlayer])
                VALUES
                (@Firstname,
                @Lastname,
                @Email,
                @PhoneNumber,
                @IsActivePlayer)";
        try
        {
            await _dataAccess.ExecuteACommand(sqlStatement,
                        new
                        {
                            player.Firstname,
                            player.Lastname,
                            player.Email,
                            player.PhoneNumber,
                            player.IsActivePlayer
                        },
                        _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;
        }
        catch (Exception ex)
        {

            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            response.ErrorMessage = ex.Message;
            return response;
        }

    }

    public async Task<ExecuteCommandResponseDto> DeletePlayer(int id)
    {
        var response = new ExecuteCommandResponseDto();
        var sqlStatement = "DELETE FROM Player WHERE id = @id";
        try
        {
            await _dataAccess.ExecuteACommand(sqlStatement, new { id }, _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;

        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            return response;
        }
    }

    public async Task<ExecuteCommandResponseDto> AmendPlayer(int id, Player playerToChange)
    {
        var response = new ExecuteCommandResponseDto();
        if (id != playerToChange.Id)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.BadData;
            response.ErrorMessage = "Id mis-match";
            return response;
        }
        var playerToUpdate = await GetPlayer(id);
        if (playerToUpdate == null)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.ResourceNotFound;
            response.ErrorMessage = "Could not find the player to update";
            return response;
        }

        var sqlStatement = "UPDATE Player SET Firstname=@Firstname, Lastname=@Lastname, Email=@Email, PhoneNumber=@PhoneNumber, IsActivePlayer=@IsActivePlayer, NumberOfPlayersRequired=@NumberOfPlayersRequired WHERE Id=@Id";
        try
        {
            await _dataAccess.ExecuteACommand(
                sqlStatement,
                new
                {
                    playerToChange.Firstname,
                    playerToChange.Lastname,
                    playerToChange.Email,
                    playerToChange.PhoneNumber,
                    playerToChange.IsActivePlayer,
                    playerToChange.Id
                },
                _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;
        }
        catch (Exception ex)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            response.ErrorMessage = ex.Message;
            return response;
        }

    }
    #endregion


    #region Fixtures Logic
    public async Task<IEnumerable<FixtureSummaryDto>> GetAllFixtures()
    {
        var sqlStatement = "SELECT * FROM Fixture";
        var fixtures = await _dataAccess.RunAQuery<Fixture, dynamic>(
            sqlStatement,
            new { },
            _connectionString);

        return (from f in fixtures
                select new FixtureSummaryDto
                {
                    Id = f.Id,
                    Opponent = f.Opponent,
                    DateOfFixture = f.DateOfFixture,
                    NumberOfPlayersRequired = f.NumberOfPlayersRequired,
                    StartTime = f.StartTime,
                    Venue = f.Venue.ToString()
                }).ToList();
    }

    public async Task<Fixture?> GetFixture(int FixtureId)
    {
        var sqlStatement = "SELECT * FROM Fixture WHERE Id = @FixtureId";
        var fixture = await _dataAccess.RunAQuery<Fixture, dynamic>(
            sqlStatement,
            new { FixtureId = @FixtureId },
            _connectionString);
        return fixture.FirstOrDefault();
    }

    public async Task<ExecuteCommandResponseDto> SaveFixture(AddFixtureDto fixtureToAdd)
    {
        var response = new ExecuteCommandResponseDto();
        var sqlStatement = "INSERT INTO Fixture(Opponent, DateOfFixture, StartTime, Venue, Postcode, NumberOfPlayersRequired) VALUES (@Opponent, @DateOfFixture, @StartTime, @Venue, @Postcode, @NumberOfPlayersRequired)";

        try
        {
            await _dataAccess.ExecuteACommand(
                               sqlStatement,
                               new
                               {
                                   fixtureToAdd.Opponent,
                                   fixtureToAdd.DateOfFixture,
                                   fixtureToAdd.StartTime,
                                   fixtureToAdd.Venue,
                                   fixtureToAdd.Postcode,
                                   fixtureToAdd.NumberOfPlayersRequired
                               },
                               _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;
        }
        catch (Exception ex)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            response.ErrorMessage = ex.Message;
            return response;
            throw;
        }


    }

    public async Task<ExecuteCommandResponseDto> DeleteFixture(int fixtureId)
    {
        var response = new ExecuteCommandResponseDto();
        var sqlStatement = "DELETE FROM Fixture WHERE id = @id";
        try
        {
            await _dataAccess.ExecuteACommand(sqlStatement, new { id = fixtureId }, _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;

        }
        catch (Exception ex)
        {
            response.ErrorMessage = ex.Message;
            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            return response;
        }
    }
    #endregion


    public async Task<ExecuteCommandResponseDto> AmendFixture(int id, Fixture fixtureToChange)
    {
        var response = new ExecuteCommandResponseDto();
        if (id != fixtureToChange.Id)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.BadData;
            response.ErrorMessage = "Id mis-match";
            return response;
        }
        var fixtureToUpdate = await GetFixture(id);
        if (fixtureToUpdate == null)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.ResourceNotFound;
            response.ErrorMessage = "Could not find the fixture to update";
            return response;
        }

        var sqlStatement = "UPDATE Fixture SET Opponent=@Opponent, DateOfFixture=@DateOfFixture, StartTime=@StartTime, Venue=@Venue, PostCode=@Postcode, NumberOfPlayersRequired=@NumberOfPlayersRequired WHERE Id=@Id";
        try
        {
            await _dataAccess.ExecuteACommand(
                sqlStatement,
                new
                {
                    fixtureToChange.Opponent,
                    fixtureToChange.DateOfFixture,
                    fixtureToChange.StartTime,
                    fixtureToChange.Venue,
                    fixtureToChange.Postcode,
                    fixtureToChange.NumberOfPlayersRequired,
                    fixtureToChange.Id
                },
                _connectionString);
            response.ExecutionStatus = Enums.ExecuteCommandEnum.Ok;
            return response;
        }
        catch (Exception ex)
        {
            response.ExecutionStatus = Enums.ExecuteCommandEnum.InternalException;
            response.ErrorMessage = ex.Message;
            return response;
        }


    }

    // availability
}
