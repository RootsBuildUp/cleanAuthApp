
using MediatR;
using CleanAuthApp.Domain.Interfaces;

namespace CleanAuthApp.Application.Command;

public class LoginHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _repo;
    private readonly IJwtService _jwt;

    public LoginHandler(IUserRepository repo, IJwtService jwt)
    {
        _repo = repo;
        _jwt = jwt;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repo.GetByUsernameAsync(request.Username);

        if (user == null || user.Password != request.Password)
            return "Invalid Credentials";

        return _jwt.GenerateToken(user.Username, user.Role);
    }
}