namespace CleanAuthApp.Application.Command;

public interface IJwtService
{
    string GenerateToken(string username, string role);
}