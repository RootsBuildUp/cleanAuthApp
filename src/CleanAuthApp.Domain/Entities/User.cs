namespace CleanAuthApp.Domain.Entities;


public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // plain for demo
    public string Role { get; set; }     // Admin / User
}