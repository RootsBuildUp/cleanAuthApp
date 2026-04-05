using CleanAuthApp.Domain.Entities;
using CleanAuthApp.Domain.Interfaces;
using MediatR;

namespace CleanAuthApp.Application.Command;

public class RegisterHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUserRepository _repo;

    public RegisterHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Password = request.Password,
            Role = request.Role
        };

        await _repo.AddUserAsync(user);
        return "User Registered";
    }
}