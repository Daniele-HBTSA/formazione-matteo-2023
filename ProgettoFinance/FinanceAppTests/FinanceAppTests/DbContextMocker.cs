using FinanceApp.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceAppTests
{
    public class DbContextMocker
    {
        public static FinanceAppContext MockedContext()
        {
            string connectionString = "Data Source=FRIDAY;Initial Catalog=FinanceAppDB;Persist Security Info=True;User ID=sa;Password=Matteo9893;Connection Timeout=900";

            var options = new DbContextOptionsBuilder<FinanceAppContext>().UseSqlServer(connectionString).Options;
        
            var dbContext = new FinanceAppContext(options);

            return dbContext;
        }
    }
}
