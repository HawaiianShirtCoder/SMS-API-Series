using Moq;
using SMS.Shared.DAL;

namespace SMS.Tests.BLL;


public class SMSLogicTests
{
    private readonly Mock<IDataAccess> _dataAccess;

    public SMSLogicTests()
    {
        _dataAccess = new Mock<IDataAccess>();
    }


}
