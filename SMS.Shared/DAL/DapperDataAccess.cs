using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SMS.Shared.DAL;

public class DapperDataAccess : IDataAccess
{

    // Getting data - SELECT statement  http Get
    public async Task<IEnumerable<T>> RunAQuery<T, U>(
       string sqlStatement,
       U parameters,
       CommandType commandType = CommandType.Text,
       string connectionString = "SMS")
    {
        // select * from player
        // select * from fixture
        // select * from player where id = ??? // should return john smith
        // give me all the players available for a given fixture - i.e 3 table join
        using IDbConnection connection = new SqlConnection(connectionString);
        return await connection.QueryAsync<T>(sqlStatement, parameters, commandType: commandType);
    }

    // Inserts (http post), Updating (http put or http patch), and deleting data (http delete)
    public async Task ExecuteACommand<T>(
       string sqlStatement,
       T parameters,
       CommandType commandType = CommandType.Text,
       string connectionString = "SMS")
    {
        // INSERT INTO Player(f1, f1, f3) VALUES (V1, V2, V3);
        // DELETE FROM PLAYER WHERE ID = 3;
        // update player set firstname = fred where id = 1
        using IDbConnection connection = new SqlConnection(connectionString);
        await connection.ExecuteAsync(sqlStatement, parameters, commandType: commandType);
    }

}
