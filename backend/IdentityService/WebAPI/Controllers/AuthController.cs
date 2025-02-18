using MediatR;
using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using IdentityService.Domain.Entities;

namespace IdentityService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<User> _userManager; // Add UserManager

    public AuthController(IMediator mediator, UserManager<User> userManager)
    {
        _mediator = mediator;
        _userManager = userManager; // Initialize UserManager
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        if (result)
            return Ok(result);
        else
            return BadRequest("Register failed");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while processing your request." });
        }
    }

    [Authorize]
    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        return Ok("Test Başarılı");
    }

    [HttpGet("username/{userId}")]
    public async Task<IActionResult> GetUsername(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId); // Use UserManager
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(new { Username = user.UserName });
    }
}