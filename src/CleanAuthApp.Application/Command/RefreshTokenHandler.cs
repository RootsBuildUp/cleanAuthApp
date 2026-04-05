
using MediatR;
using CleanAuthApp.Application.Common.Interfaces;
using CleanAuthApp.Application.DTO;

namespace CleanAuthApp.Application.Command;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IRefreshTokenService _refresh;
    private readonly IJwtService _jwt;

    public RefreshTokenHandler(IRefreshTokenService refresh, IJwtService jwt)
    {
        _refresh = refresh;
        _jwt = jwt;
    }

    public Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        if (!_refresh.Validate(request.RefreshToken, out var mobile))
            throw new Exception("Invalid or expired refresh token");

        // 🔥 IMPORTANT: Rotate refresh token
        _refresh.Remove(request.RefreshToken);

        var newAccessToken = _jwt.GenerateToken(mobile, "User");
        var newRefreshToken = _refresh.Create(mobile);

        return Task.FromResult(new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = 3600
        });
    }
}