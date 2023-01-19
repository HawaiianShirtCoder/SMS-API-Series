using SMS.Shared.DTOs.Fixtures;
using SMS.Shared.DTOs.Players;

namespace SMS.Shared.BLL
{
    public interface ISMSLogic
    {
        #region Players Logic contracts
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersSummary();
        Task SavePlayer(AddPlayerDto playerToSave);
        Task<IEnumerable<PlayerSummaryDto>> GetPlayersByStatus(bool isActivePlayer);
        #endregion


        #region Fixture logic contracts
        Task<IEnumerable<FixtureSummaryDto>> GetAllFixtures();

        Task<bool> DeleteFixture(int fixtureId);
        #endregion

    }
}