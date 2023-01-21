using Microsoft.Extensions.Configuration;
using SMS.Shared.DAL;
using SMS.Shared.DTOs;
using SMS.Shared.DTOs.Availability;
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
    #endregion

    // availability
    #region availability
    public async Task<PlayersAvailableForFixtureDto?> GetPlayersAvailableForFixture(int fixtureId)
    {
        var fixture = await GetFixture(fixtureId);
        if (fixture is null)
        {
            return null;
        }
        // Shape the fixture data into PlayersAvailableForFixtureDto
        var fixtureDetails = new PlayersAvailableForFixtureDto
        {
            FixtureId = fixture.Id,
            Opponents = fixture.Opponent,
            DateOfFixture = fixture.DateOfFixture,
            Venue = fixture.Venue,
            StartTime = fixture.StartTime
        };

        // Get the players who have signed up for the fixture....
        var sqlStatement = "SELECT p.Id AS 'playerId', p.Firstname + ' ' + p.Lastname AS 'Fullname' FROM PLayer as p JOIN Availability as a on p.Id = a.PlayerId where a.FixtureId = @fixtureId";
        var playersAvailable = await _dataAccess.RunAQuery<PlayersAvailableDto, dynamic>(sqlStatement, new { fixtureId }, _connectionString);
        fixtureDetails.AvailablePlayers = playersAvailable.ToList();
        return fixtureDetails;
    }

    public async Task<MyAvailabilitySummaryDto?> GetPlayerAvailabilitySummary(int playerId)
    {
        var player = await GetPlayer(playerId);
        if (player is null)
        {
            return null;
        }
        var me = new MyAvailabilitySummaryDto
        {
            PlayerId = player.Id,
            Fullname = $"{player.Firstname} {player.Lastname}"
        };

        var sqlStatement = "SELECT f.Id AS 'FixtureId', f.Opponent + ' (' + f.StartTime + ')' AS 'FixtureDetail'  FROM Fixture AS f JOIN Availability AS a ON f.Id = a.FixtureId WHERE a.PlayerId = @playerId";
        IEnumerable<MyFixturesDto> myFixtures;
        try
        {
            myFixtures = await _dataAccess.RunAQuery<MyFixturesDto, dynamic>(sqlStatement, new { playerId }, _connectionString);
        }
        catch (Exception ex)
        {

            throw;
        }

        if (myFixtures is null)
        {
            return null;
        }

        me.MyFixtures = myFixtures.OrderBy(f => f.DateOfFixture).ToList();

        return me;

    }

    public async Task<List<FixtureCountSummaryDto>> FixtureAvailabilityCounts()
    {
        var sqlStatement1 = "SELECT a.FixtureID, COUNT(a.FixtureID) as 'PlayersAvailableCount' FROM AVAILABILITY AS a GROUP BY a.FixtureId ORDER BY COUNT(a.FixtureID)";
        var counts = await _dataAccess.RunAQuery<FixtureCountDto, dynamic>(sqlStatement1, new { }, _connectionString);

        var allFixtures = await GetAllFixtures();
        var finalCount = new List<FixtureCountSummaryDto>();

        foreach (var fixture in allFixtures)
        {
            var playerForFixture = counts.FirstOrDefault(x => x.FixtureId == fixture.Id);
            var playersAvailable = 0;
            if (playerForFixture is not null)
            {
                playersAvailable = playerForFixture.PlayersAvailableCount;
            }
            finalCount.Add(new FixtureCountSummaryDto
            {
                FixtureId = fixture.Id,
                FixtureDetail = $"{fixture.Opponent} ({fixture.Venue}) - {fixture.StartTime}",
                DateOfFixture = fixture.DateOfFixture,
                PlayersAvailable = playersAvailable,
                PlayersRequired = fixture.NumberOfPlayersRequired
            });
        }
        return finalCount.OrderBy(fc => fc.DateOfFixture).ToList();
    }

    public async Task<ExecuteCommandResponseDto> SaveAvailability(AddAvailabilityDto input)
    {
        var response = new ExecuteCommandResponseDto();
        var sqlStatement1 = "SELECT COUNT(*) AS 'Total' FROM Availability AS A WHERE a.FixtureId = @fixtureId AND a.playerId = @playerId;";

        var total = await _dataAccess.RunAQuery<ExistingAvailabilityDto, dynamic>(
            sqlStatement1,
            new { fixtureId = input.FixtureId, playerid = input.PlayerId },
            _connectionString);

        if (total.First().Total == 0 && input.IsAvailable)
        {
            // Need to add new entry
            try
            {
                var sqlStatement2 = "INSERT INTO Availability(FixtureId, PlayerId) VALUES (@fixtureId, @playerId)";
                await _dataAccess.ExecuteACommand(
                    sqlStatement2,
                    new { fixtureId = input.FixtureId, playerId = input.PlayerId },
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
        else
        {
            if (input.IsAvailable == false && total.First().Total == 1)
            {
                try
                {
                    var sqlStatement3 = "DELETE FROM Availability WHERE FixtureId = @fixtureId AND PlayerId = @playerId";
                    await _dataAccess.ExecuteACommand(
                        sqlStatement3,
                        new { fixtureId = input.FixtureId, playerId = input.PlayerId },
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
        }
        return response;
    }
    #endregion
}
