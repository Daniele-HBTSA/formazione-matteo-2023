using FinanceApp.Context;
using FinanceApp.Controllers;
using FinanceApp.Repository.InterfacesImpl;
using FinanceApp.Services.InterfacesImpl;
using FinanceApp.Utils.Security;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Xunit;

namespace FinanceAppTests
{
    public class AuthenticationControllerTest
    {
        [Fact]
        public void TestRegistrazione()
        {
            IOptions<JwtSettings> someOptions = Options.Create<JwtSettings>(new JwtSettings());
            var context = DbContextMocker.MockedContext();
            var controller = new AuthenticationService(new AziendeRepository(context), someOptions);





        }








    }
}
