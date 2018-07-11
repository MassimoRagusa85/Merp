using System;
using Merp.Web.Site.Models;
using Microsoft.AspNetCore.Identity;

namespace Merp.Web.Site.Data
{
    public static partial class DataSeeder
    {
        public static void Seed(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (roleManager != null)
                SeedRoles(roleManager);
            else
                throw new ArgumentNullException(nameof(roleManager));

            if (userManager != null)
                SeedUsers(userManager);
            else
                throw new ArgumentNullException(nameof(userManager));
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Accountancy").Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole("Accountancy")).Result;

                if (!roleResult.Succeeded)
                    throw new InvalidOperationException("Cannot add Accountancy role.");
            }

            if (!roleManager.RoleExistsAsync("Registry").Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole("Registry")).Result;

                if (!roleResult.Succeeded)
                    throw new InvalidOperationException("Cannot add Registry role.");
            }

            if (!roleManager.RoleExistsAsync("TaskManagement").Result)
            {
                var roleResult = roleManager.CreateAsync(new IdentityRole("TaskManagement")).Result;

                if (!roleResult.Succeeded)
                    throw new InvalidOperationException("Cannot add TaskManagement role.");
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("m.r@gmail.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "Max",
                    Email = "m.r@gmail.com",
                };

                var result = userManager.CreateAsync(user, "Qwerty123@").Result;

                if (!result.Succeeded)
                    throw new InvalidOperationException($"Cannot add User '{"m.r@gmail.com"}'.");

                result = userManager.AddToRolesAsync(user, new string[] { "Accountancy", "Registry", "TaskManagement" }).Result;

                if (!result.Succeeded)
                    throw new InvalidOperationException($"Cannot add User '{"m.r@gmail.com"}' to Roles.");
            }
        }
    }
}
