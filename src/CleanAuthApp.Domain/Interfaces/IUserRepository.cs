using CleanAuthApp.Domain.Entities;

namespace CleanAuthApp.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddUserAsync(User user);
    List<User> GetAll();
}