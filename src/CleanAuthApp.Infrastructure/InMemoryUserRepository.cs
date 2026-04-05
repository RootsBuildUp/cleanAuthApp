using CleanAuthApp.Domain.Entities;
using CleanAuthApp.Domain.Interfaces;

namespace CleanAuthApp.Infrastructure;


public class InMemoryUserRepository : IUserRepository
{
    private static List<User> _users = new();

    public Task AddUserAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return Task.FromResult(_users.FirstOrDefault(x => x.Username == username));
    }

    public List<User> GetAll()
    {
        return _users;
    }
}