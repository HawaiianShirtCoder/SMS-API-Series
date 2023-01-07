using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Shared.DAL;

public class DapperDataAccess : IDataAccess
{
    private readonly IConfiguration _config;

    public DapperDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> RunAQuery<T, U>(
        string sqlStatement,
        U parameters,
        CommandType commandType = CommandType.Text,
        string connectionId = "SMS")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        return await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);
    }

    public async Task ExecuteACommand<T>(
        string sqlStatement,
        T parameters,
        CommandType commandType = CommandType.Text,
        string connectionId = "SMS")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        await connection.ExecuteAsync(sqlStatement, parameters, commandType: commandType);
    }

}
