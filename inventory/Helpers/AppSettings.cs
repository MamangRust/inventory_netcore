namespace inventory.Helpers;

public class AppSettings
{
    public required string Secret { get; set; }

    public int RefreshTokenTTL { get; set; }
}