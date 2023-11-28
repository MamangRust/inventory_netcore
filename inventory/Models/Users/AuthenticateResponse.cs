using System.Text.Json.Serialization;
using inventory.Entities;

namespace inventory.Models.Users;

public class AuthenticateResponse(User user, string jwtToken, string refreshToken)
{
    public int Id { get; set; } = user.Id;
    public string FirstName { get; set; } = user.FirstName;
    public string LastName { get; set; } = user.LastName;
    public string Email { get; set; } = user.Email;
    public string JwtToken { get; set; } = jwtToken;
    public string RefreshToken { get; set; } = refreshToken;
}