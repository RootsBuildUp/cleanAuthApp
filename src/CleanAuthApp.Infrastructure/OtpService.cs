using System.Security.Cryptography;
using System.Text;
using CleanAuthApp.Application.Common.Interfaces;
using CleanAuthApp.Domain.Entities;

namespace CleanAuthApp.Infrastructure;

public class OtpService : IOtpService
{
    private static Dictionary<string, OtpEntry> _store = new();

    public string GenerateOtp(string mobile)
    {
        var otp = Random.Shared.Next(100000, 999999).ToString();

        var hash = Hash(otp);

        _store[mobile] = new OtpEntry
        {
            MobileNumber = mobile,
            OtpHash = hash,
            Expiry = DateTime.UtcNow.AddMinutes(2),
            RetryCount = 0
        };

        return otp;
    }

    public bool VerifyOtp(string mobile, string otp)
    {
        if (!_store.ContainsKey(mobile)) return false;

        var entry = _store[mobile];

        if (entry.Expiry < DateTime.UtcNow)
            return false;

        if (entry.RetryCount >= 3)
            return false;

        if (entry.OtpHash != Hash(otp))
        {
            entry.RetryCount++;
            return false;
        }

        _store.Remove(mobile);
        return true;
    }

    private string Hash(string input)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }
}