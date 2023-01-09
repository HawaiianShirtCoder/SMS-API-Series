using SMS.Shared.DTOs;
using SMS.Shared.Models;

namespace SMS.Shared.Helper;

public static class AddPlayerDtoExtensions
{
    public static Player ToPlayerModel(this AddPlayerDto value)
    {
        return new Player
        {
            Firstname = value.Firstname,
            Lastname = value.Lastname,
            Email = value.Email,
            PhoneNumber = value.PhoneNumber,
            IsActivePlayer = value.IsActivePlayer,

        };
    }
}
