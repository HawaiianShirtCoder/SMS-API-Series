using SMS.Shared.Models;

namespace SMS.Shared.Logic
{
    public interface IPlayerLogic
    {
        Task AddPlayer(Player player);
        Task DeletePlayer(int id);
        Task<IEnumerable<Player>> GetAllPlayersSummary();
        Task<Player?> GetPlayerById(int id);
        Task UpdatePlayer(Player player);
    }
}