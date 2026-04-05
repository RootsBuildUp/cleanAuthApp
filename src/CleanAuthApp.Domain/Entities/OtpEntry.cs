namespace CleanAuthApp.Domain.Entities;

public class OtpEntry
{
    public string MobileNumber { get; set; }
    public string OtpHash { get; set; }
    public DateTime Expiry { get; set; }
    public int RetryCount { get; set; }
}