using System.ComponentModel.DataAnnotations;

namespace inventory.Models.Users;

public class AuthenticateRequest
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}