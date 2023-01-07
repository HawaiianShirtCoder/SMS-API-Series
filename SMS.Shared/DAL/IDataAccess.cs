using System.Data;

namespace SMS.Shared.DAL
{
    public interface IDataAccess
    {
        Task ExecuteACommand<T>(string sqlStatement, T parameters, CommandType commandType = CommandType.Text, string connectionId = "SMS");
        Task<IEnumerable<T>> RunAQuery<T, U>(string sqlStatement, U parameters, CommandType commandType = CommandType.Text, string connectionId = "SMS");
    }
}