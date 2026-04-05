namespace CleanAuthApp.Application.Common.Interfaces;

public interface IRefreshTokenService
{
    string Create(string mobile);
    bool Validate(string token, out string mobile);
    void Remove(string token);
}