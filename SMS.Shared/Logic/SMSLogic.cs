using Microsoft.Extensions.Configuration;
using SMS.Shared.DAL;
using SMS.Shared.DTOs.Availability;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

namespace SMS.Shared.Logic;

public class SMSLogic : ISMSLogic
{
    private readonly IDataAccess _dal;
    private readonly IConfiguration _config;
    private readonly string _connectionString = string.Empty;

    public SMSLogic(IDataAccess dal, IConfiguration config)
    {
        _dal = dal;
        _config = config;
        _connectionString = _config.GetConnectionString("SMS")!;

    }

    public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary()
    {
        var sqlStatement = "select * from [dbo].[Player];";
        var results = await _dal.RunAQuery<PlayerSummaryDto, dynamic>(
            sqlStatement,
            new { },
            _connectionString);
        return results;
    }

    public async Task<Player?> GetPlayerById(int id)
    {
        var sqlStatement = "select * from [dbo].[Player] where [id]=@id;";
        var results = await _dal.RunAQuery<Player, dynamic>(
            sqlStatement,
            new { id },
            _connectionString);
        return results.FirstOrDefault();
    }

    public async Task AddPlayer(Player player)
    {
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
        await _dal.ExecuteACommand(sqlStatement,
            new
            {
                player.Firstname,
                player.Lastname,
                player.Email,
                player.PhoneNumber,
                player.IsActivePlayer
            },
            _connectionString);
    }

    public async Task UpdatePlayer(Player player)
    {
        var sqlStatement =
            "update [dbo].[Player]" +
            " set [Firstname]=@Firstname, [Lastname]=@Lastname," +
            " [Email]=@Email, [PhoneNumber]=@PhoneNumber, [IsActivePlayer]=@IsActivePlayer" +
            " where [id]=@id";
        await _dal.ExecuteACommand(sqlStatement, player, _connectionString);
    }

    public async Task DeletePlayer(int id)
    {
        var sqlStatement = "delete from [dbo].[Player] where [id] = @Id";
        await _dal.ExecuteACommand(sqlStatement, new { Id = id }, _connectionString);
    }


    // Extra services added....
    // Contrived method to demo a piece of business logic using an existing dal call
    public async Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActive)
    {
        var players = await GetPlayersSummary();
        return players.Where(x => x.IsActivePlayer == isActive).ToList();
    }

    public async Task<IEnumerable<MyAvailabilitySummaryDto>> GetPlayerAvailabilitySummary(int playerId)
    {
        //TODO:  NOT COMPLETE YET
        //var sqlStatement = "";
        //var results = await _dal.RunAQuery<MyAvailabilitySummaryDto, dynamic>(
        //   sqlStatement,
        //   new { playerId },
        //   _connectionString);
        //return results.FirstOrDefault();
        throw new NotImplementedException();

    }

    public async Task<bool> AddAvailability(AddAvailabilityDto myAvailability)
    {
        //TODO:
        try
        {
            var sqlStatement = "";
            await _dal.ExecuteACommand(sqlStatement, new { }, _connectionString);
            return true;
        }
        catch (Exception)
        {

            // log the error
            return false;
        }
    }


    //TODO:  Fixture methods
}
