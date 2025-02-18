using MediatR;
using IdentityService.Domain.Entities;
using IdentityService.Application.Commands;
using IdentityService.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace IdentityService.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IConfiguration configuration,
        UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username");
        }

        var result = await _userRepository.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
         var userRoles = await _userManager.GetRolesAsync(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "default_key_here");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(userRoles.Select(role => new Claim(ClaimTypes.Role, role)).Append(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)).Append(new Claim(ClaimTypes.NameIdentifier, user.Id))),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}