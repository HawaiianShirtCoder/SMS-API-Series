// See https://aka.ms/new-console-template for more information
using SMS.Shared.DAL;
using SMS.Shared.DTOs.Players;
using SMS.Shared.Models;

//We won't do this in our finished web api - hardcoding not a good idea.
var connectionString = @"Data Source=JASONSURFACE\SQLEXPRESS;Initial Catalog=SMS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


// We won't use this approach in our web api as this is coupling this application to a concrete implementation
// REMEMBER : "NEW" IS "GLUE"!
// Good stuff to come in future video programming against an interface rather than an implementation (DI)
var dataAccess = new DapperDataAccess();


// Example 1 - Get all players
//Console.WriteLine("**** GET all players from the database ****");
//var sqlStatement = "SELECT * FROM [Player];";

//var playersFromDatabase = await dataAccess.RunAQuery<Player, dynamic>(
//    sqlStatement,
//    new { },
//    commandType: System.Data.CommandType.Text,
//    connectionString: connectionString);

//foreach (var p in playersFromDatabase)
//{
//    Console.WriteLine($"{p.Firstname}, {p.Lastname}, {p.Email}, {p.PhoneNumber}, {p.IsActivePlayer}");
//}

//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

//Example 2 Get 1 player from the database
//Console.WriteLine("Please enter player Id:");
//var id = Console.ReadLine();
//var sqlStatement2 = "SELECT * FROM Player WHERE id = @id";

//var playerFromDatabase = await dataAccess.RunAQuery<Player, dynamic>(
//    sqlStatement2,
//    new { id = id },
//    commandType: System.Data.CommandType.Text,
//    connectionString: connectionString);

//var p = playerFromDatabase.FirstOrDefault();
//Console.WriteLine($"{p.Firstname}, {p.Lastname}, {p.Email}, {p.PhoneNumber}, {p.IsActivePlayer}");
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

//Example 3 - Add a new player to the database
//Console.WriteLine("**** Add a player to the database ****");

//var addPlayerDto = GetUserInputToAddNewPlayer();

//var sqlStatement3 = @"INSERT INTO Player(Firstname, Lastname, Email, PhoneNumber, IsActivePlayer) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @IsActivePlayer)";

//try
//{
//    await dataAccess.ExecuteACommand<Player>(
//        sqlStatement3,
//        new Player
//        {
//            Firstname = addPlayerDto.Firstname,
//            Lastname = addPlayerDto.Lastname,
//            Email = addPlayerDto.Email,
//            PhoneNumber = addPlayerDto.PhoneNumber,
//            IsActivePlayer = addPlayerDto.IsActivePlayer,
//        },
//        System.Data.CommandType.Text,
//        connectionString);
//    Console.WriteLine("Inserted new player");
//}
//catch (Exception)
//{
//    Console.WriteLine("Something went wrong with insert!");

//}
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

//Example 4 - deleting a record from the system
Console.WriteLine("**** Delete a player to the database ****");
Console.WriteLine("Provide Player Id you wish to delete:");
var id = Console.ReadLine();

var sqlStatement4 = "DELETE FROM Player WHERE id = @id";

try
{
    await dataAccess.ExecuteACommand<Player>(
        sqlStatement4,
        new Player
        {
            Id = Convert.ToInt32(id)
        },
        System.Data.CommandType.Text,
        connectionString);

    Console.WriteLine("Player has been deleted from the db");
}
catch (Exception)
{
    Console.WriteLine("Something went wrong with delete!");
}

Console.ReadLine();

static AddPlayerDto GetUserInputToAddNewPlayer()
{
    Console.WriteLine("Enter Firstname");
    string? firstname = Console.ReadLine();
    Console.WriteLine("Enter Lastname");
    string? lastName = Console.ReadLine();
    Console.WriteLine("Enter Email");
    string? email = Console.ReadLine();
    Console.WriteLine("Enter Phone");
    string? phoneNumber = Console.ReadLine();
    Console.WriteLine("Is Active (true or false)");
    var isActivePlayer = Console.ReadLine() ?? "true";
    return new AddPlayerDto
    {
        Firstname = firstname ?? string.Empty,
        Lastname = lastName ?? string.Empty,
        Email = email ?? string.Empty,
        PhoneNumber = phoneNumber ?? string.Empty,
        IsActivePlayer = bool.Parse(isActivePlayer)
    };
}