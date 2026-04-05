
using System.Security.Cryptography;
using CleanAuthApp.Application.Common.Interfaces;
using CleanAuthApp.Domain.Entities;

namespace CleanAuthApp.Infrastructure;

public class RefreshTokenService : IRefreshTokenService
{
    private static Dictionary<string, RefreshTokenEntry> _store = new();

    public string Create(string mobile)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        _store[token] = new RefreshTokenEntry
        {
            Token = token,
            MobileNumber = mobile,
            Expiry = DateTime.UtcNow.AddDays(7)
        };

        return token;
    }
    
    public bool Validate(string token, out string mobile)
    {
        mobile = null;

        if (!_store.ContainsKey(token))
            return false;

        var entry = _store[token];

        if (entry.Expiry < DateTime.UtcNow)
        {
            _store.Remove(token);
            return false;
        }

        mobile = entry.MobileNumber;
        return true;
    }

    public void Remove(string token)
    {
        if (_store.ContainsKey(token))
            _store.Remove(token);
    }
}