namespace CleanAuthApp.Domain.Entities;

public class AnonymousSession
{
    public string Token { get; set; }
    public string DeviceId { get; set; }
    public DateTime Expiry { get; set; }
    public int RequestCount { get; set; }
}