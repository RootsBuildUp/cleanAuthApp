
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanAuthApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok("Admin Access");
    }

    [HttpGet("user")]
    [Authorize(Roles = "User")]
    public IActionResult UserOnly()
    {
        return Ok("User Access");
    }
}