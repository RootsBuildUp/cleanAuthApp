namespace CleanAuthApp.Application.Common.Interfaces;

public interface IAnonymousTokenService
{
    string Create(string deviceId);
    bool Validate(string token, string deviceId);
    void IncreaseRequest(string token);
}