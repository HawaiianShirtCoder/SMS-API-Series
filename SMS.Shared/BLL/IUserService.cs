namespace SMS.Shared.BLL
{
    public interface IUserService
    {
        string HashPassword(string username, string password);
        bool VerifyPassword(string username, string hashedPassword, string password);
    }
}