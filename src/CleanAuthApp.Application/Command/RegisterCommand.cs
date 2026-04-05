using MediatR;

namespace CleanAuthApp.Application.Command;

public record RegisterCommand(string Username, string Password, string Role) : IRequest<string>;