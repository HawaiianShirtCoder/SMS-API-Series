﻿using Microsoft.Extensions.Configuration;
using SMS.Shared.DAL;
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

    // players
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

    public async Task SavePlayer(AddPlayerDto playerToSave)
    {
        // pre - processing example
        // map addPlayerDto to player
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
    }



    // fixtures

    // availability
}