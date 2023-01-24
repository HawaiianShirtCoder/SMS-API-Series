using System.Data;

namespace SMS.Shared.DAL;


public class SqlLiteDapperDataAccess : IDataAccess
{
    public Task ExecuteACommand<T>(string sqlStatement, T parameters, string connectionString, CommandType commandType = CommandType.Text)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> RunAQuery<T, U>(string sqlStatement, U parameters, string connectionString, CommandType commandType = CommandType.Text)
    {
        throw new NotImplementedException();
    }
}
