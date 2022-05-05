using Microsoft.AspNetCore.Identity;
using NetworkOfShops.Models;

namespace NetworkOfShops.Data
{
    public interface IAuthorizationInitializer
    {
        Task GenerateAdminAndRoles();
    }
    public class AuthorizationInitializer : IAuthorizationInitializer
    {
        private readonly SignInManager<AplicationUser> _signInManager;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleMannager;
        public AuthorizationInitializer(SignInManager<AplicationUser> signInManager, UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleMannager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleMannager = roleMannager;
        }
        public async Task GenerateAdminAndRoles()
        {
            string adminRole = "Admin";
            string managerRole = "Manager";
            string staffRole = "Staff";
            string clientRole = "Client";

            if (!await _roleMannager.RoleExistsAsync(adminRole))
            {
                await _roleMannager.CreateAsync(new IdentityRole(adminRole));
            }

            if (!await _roleMannager.RoleExistsAsync(managerRole))
            {
                await _roleMannager.CreateAsync(new IdentityRole(managerRole));
            }

            if (!await _roleMannager.RoleExistsAsync(staffRole))
            {
                await _roleMannager.CreateAsync(new IdentityRole(staffRole));
            }

            if (!await _roleMannager.RoleExistsAsync(clientRole))
            {
                await _roleMannager.CreateAsync(new IdentityRole(clientRole));
            }

            var admin = new AplicationUser()
            {
                SecurityStamp = new Guid().ToString(),
                UserName = "admin@test.pl",
                Email = "admin@test.pl",
            };

            if (await _userManager.FindByNameAsync(admin.UserName) == null)
            {
                await _userManager.CreateAsync(admin, "Pass4Admin!");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(admin);
                await _userManager.ConfirmEmailAsync(admin, code);
                await _userManager.AddToRoleAsync(admin, adminRole);
                await _userManager.AddToRoleAsync(admin, managerRole);
                await _userManager.AddToRoleAsync(admin, staffRole);
                await _userManager.AddToRoleAsync(admin, clientRole);
            }

            var manager = new AplicationUser()
            {
                SecurityStamp = new Guid().ToString(),
                UserName = "manager@test.pl",
                Email = "manager@test.pl",
            };

            if (await _userManager.FindByNameAsync(manager.UserName) == null)
            {
                await _userManager.CreateAsync(manager, "Pass4Manager!");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(manager);
                await _userManager.ConfirmEmailAsync(manager, code);
                await _userManager.AddToRoleAsync(manager, managerRole);
            }

            var staff = new AplicationUser()
            {
                SecurityStamp = new Guid().ToString(),
                UserName = "staff@test.pl",
                Email = "staff@test.pl",
            };

            if (await _userManager.FindByNameAsync(staff.UserName) == null)
            {
                await _userManager.CreateAsync(staff, "Pass4Staff!");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(staff);
                await _userManager.ConfirmEmailAsync(staff, code);
                await _userManager.AddToRoleAsync(staff, staffRole);
            }

            var client = new AplicationUser()
            {
                SecurityStamp = new Guid().ToString(),
                UserName = "client@test.pl",
                Email = "client@test.pl",
            };

            if (await _userManager.FindByNameAsync(client.UserName) == null)
            {
                await _userManager.CreateAsync(client, "Pass4AClient!");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(client);
                await _userManager.ConfirmEmailAsync(client, code);
                await _userManager.AddToRoleAsync(client, clientRole);
            }
        }
    }
}
