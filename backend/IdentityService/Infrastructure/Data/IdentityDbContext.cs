 using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
 using Microsoft.EntityFrameworkCore;
 using IdentityService.Domain.Entities;

 namespace IdentityService.Infrastructure.Data
 {
     public class IdentityDbContext : IdentityDbContext<User, IdentityRole, string>
     {
         public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
         {
         }
     }
 }