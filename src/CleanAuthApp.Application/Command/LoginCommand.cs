namespace CleanAuthApp.Application.Command;

using MediatR;

public record LoginCommand(string Username, string Password) : IRequest<string>;