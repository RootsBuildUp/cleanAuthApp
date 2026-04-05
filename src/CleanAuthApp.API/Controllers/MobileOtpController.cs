using CleanAuthApp.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace CleanAuthApp.API.Controllers;

[ApiController]
[Route("api/mobile-otp")]
public class MobileOtpController : ControllerBase
{
    private readonly IMediator _mediator;

    public MobileOtpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestOtp(RequestOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyOtp(VerifyOtpCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}