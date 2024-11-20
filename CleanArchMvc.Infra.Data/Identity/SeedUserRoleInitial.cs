using System;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;
public class SeedUserRoleInitial
{
    private readonly MongoDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(MongoDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedRoles()
    {
        string[] roleNames = { "Admin", "User", "Manager" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }

    public async Task SeedUsers()
    {
        var user = await _userManager.FindByEmailAsync("admin@example.com");
        if (user == null)
        {
            var newUser = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };
            var result = await _userManager.CreateAsync(newUser, "Password123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "Admin");
            }
        }
    }
}

