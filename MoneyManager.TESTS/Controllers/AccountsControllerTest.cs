using Autofac;
using Autofac.Extras.Moq;
using money_manager_api.Controllers;
using money_manager_api.Entities;
using money_manager_api.Helpers;
using money_manager_api.Services;
using MoneyManager.TESTS.Setup;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MoneyManager.TESTS.Controllers
{
    public class AccountsControllerTest
    {
        [Fact]
        public async Task Get_Top_1()
        {
            using (var dataContextMock = DbContextMocker.GetEmptyDataContext())
            {
                var account = new Account
                {

                };

                dataContextMock.Accounts.Add(Account);
                dataContextMock.SaveChanges();

                using (var mock = AutoMock.GetLoose(cfg => cfg.RegisterInstance(dataContextMock).As<DataContext>()))
                {
                    // arrange
                    var accountService = mock.Create<AccountService>();

                    var controller = new AccountsController(accountService);

                    // act
                    var actionResult = controller.GetAll().Take(1);

                    // assert
                    Assert.Single(actionResult.ToList());
                }
            }
        }


    }
}
