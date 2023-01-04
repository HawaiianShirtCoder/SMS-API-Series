using SMS.Shared.Models;

namespace SMS.API.Fake_Database;

public class InMemoryDatabase
{
    public static List<Player> Players = new List<Player>()
        {
            new Player
            {
                Id = 1,
                Firstname = "John",
                Lastname = "Smith",
                Email = "JSmith@web.com",
                PhoneNumber = "123456789",
                IsActivePlayer = true
            },

            new Player
            {
                Id = 2,
                Firstname = "Peter",
                Lastname = "Parker",
                Email = "PParker@web.com",
                PhoneNumber = "999",
                IsActivePlayer = false
            },

            new Player
            {
                Id = 3,
                Firstname = "Ben",
                Lastname = "Stokes",
                Email = "BStokes@web.com",
                PhoneNumber = "999",
                IsActivePlayer = true
            }

        };
}

