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

    public static List<Fixture> Fixtures = new List<Fixture>()
    {
        new Fixture
        {
            Id = 1,
            Opponent = "Southampton",
            DateOfFixture= new DateTime(2023,1,23),
            Venue = "Home",
            StartTime = "15:00",
            Postcode = "n/a",
            NumberOfPlayersRequired = 11
        },
        new Fixture
        {
            Id = 2,
            Opponent = "Liverpool",
            DateOfFixture= new DateTime(2023,1,24),
            Venue = "Away",
            StartTime = "15:00",
            Postcode = "postcode1",
            NumberOfPlayersRequired = 11
        },
        new Fixture
        {
            Id = 3,
            Opponent = "West Ham",
            DateOfFixture= new DateTime(2023,1,25),
            Venue = "Away",
            StartTime = "17:00",
            Postcode = "postcode2",
            NumberOfPlayersRequired = 11
        }
    };
}

