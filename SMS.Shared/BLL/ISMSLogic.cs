using SMS.Shared.DTOs.Players;

namespace SMS.Shared.BLL
{
    public interface ISMSLogic
    {
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary();
        Task SavePlayer(AddPlayerDto playerToSave);
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActivePlayer);
    }
}