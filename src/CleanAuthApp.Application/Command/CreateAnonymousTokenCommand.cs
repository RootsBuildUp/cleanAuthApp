namespace CleanAuthApp.Application.Command;

using MediatR;

public record CreateAnonymousTokenCommand(string DeviceId) : IRequest<string>;