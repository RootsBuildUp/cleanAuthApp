using MediatR;
using CleanAuthApp.Application.Common.Interfaces;

namespace CleanAuthApp.Application.Command;

public class RequestOtpHandler : IRequestHandler<RequestOtpCommand, string>
{
    private readonly IAnonymousTokenService _anon;
    private readonly IOtpService _otp;

    public RequestOtpHandler(IAnonymousTokenService anon, IOtpService otp)
    {
        _anon = anon;
        _otp = otp;
    }

    public Task<string> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
    {
        if (!_anon.Validate(request.AnonymousToken, "device123"))
            return Task.FromResult("Invalid Anonymous Token");

        var otp = _otp.GenerateOtp(request.MobileNumber);

        // simulate send SMS
        Console.WriteLine($"OTP for {request.MobileNumber}: {otp}");

        return Task.FromResult("OTP Sent");
    }
}