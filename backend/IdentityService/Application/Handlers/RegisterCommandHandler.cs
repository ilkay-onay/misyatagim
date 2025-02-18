using MediatR;
using IdentityService.Domain.Entities;
using IdentityService.Application.Commands;
using IdentityService.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace IdentityService.Application.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RegisterCommandHandler(
        IUserRepository userRepository,
        RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.Username,
            Email = request.Username,
        };

        // Use UserManager to create the user and hash the password
        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return false;
        }

        // Create the requested role if it doesn't exist
        if (!await _roleManager.RoleExistsAsync(request.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(request.Role));
        }
     
        // Assign the requested role
         var addRoleResult = await _userManager.AddToRoleAsync(user, request.Role);
         if (!addRoleResult.Succeeded)
         {
           return false;
         }
         
      // Check if the default "User" role exists
        if (!await _roleManager.RoleExistsAsync("User"))
        {
           await _roleManager.CreateAsync(new IdentityRole("User"));
        }
    
    //Only assign default role if role is user or admin isn't chosen
    if (request.Role == "user" && !await _userManager.IsInRoleAsync(user, "User"))
     {
         await _userManager.AddToRoleAsync(user, "User");
      }

        return true;
    }
}