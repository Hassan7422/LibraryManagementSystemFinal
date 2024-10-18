using LMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace LMS.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    //Roles
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    if (!await roleManager.RoleExistsAsync(UserRoles.User))
                        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                    //Users
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    string adminUserEmail = "abdo7amdy123@gmail.com";

                    var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                    if (adminUser == null)
                    {
                        var newAdminUser = new AppUser()
                        {
                            UserName = "Abdelrhman hamdy",
                            Email = adminUserEmail,
                            EmailConfirmed = true,
                            Address="Qena",
                            FirstName= "Abdelrhman",
                            LastName="Hamdy",
                            NationalID="30309092701099",
                            BitrthDate= new DateTime(2003, 9, 9),
                        };
                        await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                    }

                    string appUserEmail = "mohammedAbdelrazik@gmail.com";

                    var appUser = await userManager.FindByEmailAsync(appUserEmail);
                    if (appUser == null)
                    {
                        var newAppUser = new AppUser()
                        {
                            UserName = "Mohmmed",
                            Email = appUserEmail,
                            EmailConfirmed = true,
                            Address = "Qena",
                            FirstName = "Mohammed",
                            LastName = "Ahmed",
                            NationalID = "30309092701098",
                            BitrthDate = new DateTime(2003, 8, 8),
                        };
                        await userManager.CreateAsync(newAppUser, "Coding@1234?");
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                    }
                }       
        }
    }
}
