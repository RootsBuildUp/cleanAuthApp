using MediatR;
using CleanAuthApp.Application.Common.Interfaces;

namespace CleanAuthApp.Application.Command;

public class CreateAnonymousTokenHandler 
    : IRequestHandler<CreateAnonymousTokenCommand, string>
{
    private readonly IAnonymousTokenService _service;

    public CreateAnonymousTokenHandler(IAnonymousTokenService service)
    {
        _service = service;
    }

    public Task<string> Handle(CreateAnonymousTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _service.Create(request.DeviceId);
        return Task.FromResult(token);
    }
}