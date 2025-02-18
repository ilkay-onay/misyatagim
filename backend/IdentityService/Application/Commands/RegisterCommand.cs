using MediatR;

namespace IdentityService.Application.Commands;

public class RegisterCommand : IRequest<bool>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
}