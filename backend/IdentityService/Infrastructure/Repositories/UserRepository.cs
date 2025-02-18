  using IdentityService.Application.Interfaces;
    using IdentityService.Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    namespace IdentityService.Infrastructure.Repositories
    {
        public class UserRepository : IUserRepository
        {
            private readonly UserManager<User> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
             }

           public async Task<User?> GetByUsernameAsync(string username)
          {
             return await _userManager.FindByNameAsync(username);
          }

            public async Task AddAsync(User user)
            {
                 var result = await _userManager.CreateAsync(user, user.PasswordHash);
                 if (!result.Succeeded)
                 {
                     throw new Exception(string.Join(",",result.Errors.Select(e=>e.Description)));
                 }
           }

           public IQueryable<User> GetAllAsync()
            {
                 return  _userManager.Users;
             }

             public async Task AddToRoleAsync(User user, string role)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
         return result; //Debug ederken bu satıra dikkat edin.
    }
        }
    }