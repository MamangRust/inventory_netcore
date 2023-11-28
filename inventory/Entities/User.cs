using System.Text.Json.Serialization;

namespace inventory.Entities;

public class User : DateTimeInfo
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }

    [JsonIgnore]
    public required string PasswordHash { get; set; }

    [JsonIgnore]
    public List<RefreshToken> RefreshTokens { get; set; } = null!;
}