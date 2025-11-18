using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Constants;
using Domain.Entites;
using Infrastructure.Constants;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Seeders
{
    public class Seeder : ISeeder
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public Seeder(AppDbContext appDbContext, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task SeedAsync()
        {
            if (await _appDbContext.Database.CanConnectAsync())
            {
                if(!await _appDbContext.Roles.AnyAsync())
                {
                    var adminRole = new IdentityRole
                    {
                        Name = Roles.Admin,
                        NormalizedName = Roles.Admin.ToUpper()
                    };
                    var memberRole = new IdentityRole
                    {
                        Name = Roles.Member,
                        NormalizedName = Roles.Member.ToUpper()
                    };
                    await _roleManager.CreateAsync(adminRole);
                    await _roleManager.CreateAsync(memberRole);
                }
                if (!await _appDbContext.Users.AnyAsync())
                {
                    var adminUser = new ApplicationUser
                    {
                        Email = DefaultUser.Email,
                        FirstName = DefaultUser.FirstName,
                        LastName = DefaultUser.LastName,
                        UserName = DefaultUser.Email,
                        EmailConfirmed = DefaultUser.EmailConfirmed
                    };
                    var password = _configuration.GetSection(ConfigurationConstants.DefaultAdmin).GetValue<string>(ConfigurationConstants.Password);
                    var result = await _userManager.CreateAsync(adminUser, password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create default admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                    await _userManager.AddToRoleAsync(adminUser, Roles.Admin);
                }
            }
        }
    }
}
