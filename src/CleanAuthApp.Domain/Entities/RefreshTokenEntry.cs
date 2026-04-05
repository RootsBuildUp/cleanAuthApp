namespace CleanAuthApp.Domain.Entities;

public class RefreshTokenEntry
{
    public string Token { get; set; }
    public string MobileNumber { get; set; }
    public DateTime Expiry { get; set; }
}