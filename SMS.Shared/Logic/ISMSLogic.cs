using SMS.Shared.DTOs.Availability;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

namespace SMS.Shared.Logic
{
    public interface ISMSLogic
    {
        Task<bool> AddAvailability(AddAvailabilityDto myAvailability);
        Task AddPlayer(Player player);
        Task DeletePlayer(int id);
        Task<IEnumerable<MyAvailabilitySummaryDto>> GetPlayerAvailabilitySummary(int playerId);
        Task<Player?> GetPlayerById(int id);
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActive);
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary();
        Task UpdatePlayer(Player player);
    }
}