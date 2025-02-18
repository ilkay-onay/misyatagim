using IdentityService.Domain.Entities;
 using System.Linq;
namespace IdentityService.Application.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    IQueryable<User> GetAllAsync();
    Task AddToRoleAsync(User user, string role);
    Task<bool> CheckPasswordAsync(User user, string password);
}