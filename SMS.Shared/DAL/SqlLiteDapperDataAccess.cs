using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace SMS.Shared.DAL;


public class SqlLiteDapperDataAccess : IDataAccess
{
    public async Task ExecuteACommand<T>(string sqlStatement, T parameters, string connectionString, CommandType commandType = CommandType.Text)
    {
        using IDbConnection connection = new SqliteConnection(connectionString);
        await connection.ExecuteAsync(sqlStatement, parameters, commandType: commandType);
    }

    public async Task<IEnumerable<T>> RunAQuery<T, U>(string sqlStatement, U parameters, string connectionString, CommandType commandType = CommandType.Text)
    {
        try
        {
            using IDbConnection connection = new SqliteConnection(connectionString);
            return await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);
        }
        catch (Exception ex)
        {

            throw;
        }

    }
}
