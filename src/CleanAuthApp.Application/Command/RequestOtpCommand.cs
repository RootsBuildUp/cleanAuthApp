namespace CleanAuthApp.Application.Command;

using MediatR;
public record RequestOtpCommand(string AnonymousToken, string MobileNumber) : IRequest<string>;