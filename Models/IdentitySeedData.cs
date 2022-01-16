using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MaxPizzaProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Advanced.Models
{
    public class IdentitySeedData
    {
        private static IdentityContext context;
        //static IdentitySeedData(IdentityContext identCtx)
        //{
         //   context = identCtx;
        //}
        public static void CreateAdminAccount(IServiceProvider serviceProvider,
                                              IConfiguration configuration,IdentityContext identCtx)
        {
            context = identCtx;
            if (context.Database.GetPendingMigrations().Any())
            {

                //Using All Waiting Migrations.(Like Update)
                //dotnet ef database update --context [DBContext]
                context.Database.Migrate();
            }

            CreateAdminAccountAsycn(serviceProvider, configuration).Wait();
        }


        public static async Task CreateAdminAccountAsycn(IServiceProvider serviceProvider,
                                                         IConfiguration configuration)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;

            UserManager<IdentityUser> userManager = serviceProvider.
                                                        GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.
                                                    GetRequiredService<RoleManager<IdentityRole>>();

            string username = configuration["Data:AdminUser:Name"] ?? "*****";

            string email = configuration["Data:AdminUser:Email"] ?? "admin@gmail.com";

            string password = configuration["Data:AdminUser:Password"] ?? "******";

            string role = configuration["Data:AdminUser:Role"] ?? "Admin";

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
