using CleanAuthApp.Application.DTO;
using MediatR;

namespace CleanAuthApp.Application.Command;

public record VerifyOtpCommand(string AnonymousToken, string MobileNumber, string Otp)
    : IRequest<AuthResponse>;