using System.Security.Cryptography;
using CleanAuthApp.Application.Common.Interfaces;
using CleanAuthApp.Domain.Entities;

namespace CleanAuthApp.Infrastructure.Services;

public class AnonymousTokenService : IAnonymousTokenService
{
    private static Dictionary<string, AnonymousSession> _store = new();

    public string Create(string deviceId)
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        _store[token] = new AnonymousSession
        {
            Token = token,
            DeviceId = deviceId,
            Expiry = DateTime.UtcNow.AddMinutes(5),
            RequestCount = 0
        };

        return token;
    }

    public bool Validate(string token, string deviceId)
    {
        if (!_store.ContainsKey(token))
            return false;

        var session = _store[token];

        // Expired
        if (session.Expiry < DateTime.UtcNow)
        {
            _store.Remove(token);
            return false;
        }

        // Device mismatch
        if (session.DeviceId != deviceId)
            return false;

        return true;
    }

    public void IncreaseRequest(string token)
    {
        if (_store.ContainsKey(token))
        {
            _store[token].RequestCount++;

            if (_store[token].RequestCount > 5)
            {
                throw new Exception("Too many requests");
            }
        }
    }
}