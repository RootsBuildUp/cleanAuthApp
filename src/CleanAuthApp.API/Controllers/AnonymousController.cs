using CleanAuthApp.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanAuthApp.API.Controllers;

[ApiController]
[Route("api/anonymous")]
public class AnonymousController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnonymousController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("token")]
    public async Task<IActionResult> CreateToken([FromBody] string deviceId)
    {
        var token = await _mediator.Send(new CreateAnonymousTokenCommand(deviceId));
        return Ok(new { token });
    }
}