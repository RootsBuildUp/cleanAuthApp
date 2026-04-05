using MediatR;
using CleanAuthApp.Application.Common.Interfaces;
using CleanAuthApp.Application.DTO;

namespace CleanAuthApp.Application.Command;

public class VerifyOtpHandler : IRequestHandler<VerifyOtpCommand, AuthResponse>
{
    private readonly IAnonymousTokenService _anon;
    private readonly IOtpService _otp;
    private readonly IJwtService _jwt;
    private readonly IRefreshTokenService _refresh;

    public VerifyOtpHandler(
        IAnonymousTokenService anon,
        IOtpService otp,
        IJwtService jwt,
        IRefreshTokenService refresh)
    {
        _anon = anon;
        _otp = otp;
        _jwt = jwt;
        _refresh = refresh;
    }

    public Task<AuthResponse> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        if (!_anon.Validate(request.AnonymousToken, "device123"))
            throw new Exception("Invalid Anonymous Token");

        if (!_otp.VerifyOtp(request.MobileNumber, request.Otp))
            throw new Exception("Invalid OTP");

        var accessToken = _jwt.GenerateToken(request.MobileNumber, "User");
        var refreshToken = _refresh.Create(request.MobileNumber);

        return Task.FromResult(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 3600
        });
    }
}