using Microsoft.EntityFrameworkCore;
using money_manager_api.Entities;
using money_manager_api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyManager.TESTS.Setup
{
    public class DbContextMocker
    {
        public async Task<DataContext> GetFakeDataContextAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            await context.Accounts.AddAsync(
                new Account
                {

                }
            );

            await context.SaveChangesAsync();

            return context;
        }

        public static DataContext GetFakeDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new DataContext(options);

            context.Permissions.Add(
                new Permission
                {
                    Id = 1,
                    Name = "permission",
                    Description = "permission description"
                }
            );

            var currencyId = Guid.NewGuid();

            context.Currencies.Add(
                new Currency
                {
                    Id = currencyId,
                    Iso = "EUR",
                    Name = "Euro"
                }
            );

            var colorId = Guid.NewGuid();

            context.Colors.Add(
                new Color
                {
                    Id = colorId,
                    Name = "color",
                    HexadecimalValue = "ffffff"
                }
            );

            var userId = Guid.NewGuid();

            context.Users.Add(
                new User
                {
                    Id = userId,
                    Email = "user@mail.com",
                    FirstName = "firstName",
                    LastName = "lastName",
                    Username = "username",
                    PermissionId = 1
                }
            );

            var accountId = Guid.NewGuid();

            context.Accounts.Add(
                new Account
                {
                    Id = accountId,
                    UserId = userId,
                    Name = "account",
                    CurrencyId = currencyId,
                    ColorId = colorId,
                    UseInStatistics = true,
                    Archived = false
                }
            );

            context.SaveChanges();

            return context;
        }

        public static DataContext GetEmptyDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }
    }
}
