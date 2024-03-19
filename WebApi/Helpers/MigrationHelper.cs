using Microsoft.EntityFrameworkCore;
using WebApi.EFConfig;
using WebApi.Entities;
using WebApi.Entities.Enums;

namespace WebApi.Helpers
{
    public static class MigrationHelper
    {
        public static async Task MigrateAsync(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            if (!await context.Companies.AnyAsync())
            {
                await SeedAsync(context);
            }
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
            // Create company
            var companies = new List<Company>
            {
                new()
                {
                    Name = "Vivasoft Ltd",
                    Address = "Bobani",
                    Employees =
                    [
                        new Employee
                        {
                            Name = "Emp 1",
                            EmployeeId = 1,
                            Level = Level.SDEL1,
                            Salary = 20000
                        },
                    ],
                    BonusList =
                    [
                        new Bonus
                        {
                            Title = "Eid Ul Azaha",
                            Month = Month.Apr,
                            Active = true,
                            AmountPercentage = 40,
                            EmployeeLevel = Level.SDEL1,
                        }
                    ]
                },
                new() { Name = "Green Feather Technologies" },
            };

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();
        }
    }
}
