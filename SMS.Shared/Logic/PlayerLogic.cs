using SMS.Shared.DAL;
using SMS.Shared.Models;

namespace SMS.Shared.Logic;

public class PlayerLogic : IPlayerLogic
{
    private readonly IDataAccess _dal;

    public PlayerLogic(IDataAccess dal)
    {
        _dal = dal;
    }

    public async Task<IEnumerable<Player>> GetAllPlayersSummary()
    {
        var sqlStatement = "select * from [dbo].[Player];";
        var results = await _dal.RunAQuery<Player, dynamic>(
            sqlStatement,
            new { });
        return results;
    }

    public async Task<Player?> GetPlayerById(int id)
    {
        var sqlStatement = "select * from [dbo].[Player] where [id]=@id;";
        var results = await _dal.RunAQuery<Player, dynamic>(
            sqlStatement,
            new { id });
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
            });
    }

    public async Task UpdatePlayer(Player player)
    {
        var sqlStatement =
            "update [dbo].[Player]" +
            " set [Firstname]=@Firstname, [Lastname]=@Lastname," +
            " [Email]=@Email, [PhoneNumber]=@PhoneNumber, [IsActivePlayer]=@IsActivePlayer" +
            " where [id]=@id";
        await _dal.ExecuteACommand(sqlStatement, player);
    }

    public async Task DeletePlayer(int id)
    {
        var sqlStatement = "delete from [dbo].[Player] where [id] = @Id";
        await _dal.ExecuteACommand(sqlStatement, new { Id = id });
    }
}
