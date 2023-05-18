using FinanceApp.Context;

namespace FinanceApp.Repository.InterfacesImpl
{
    public class MovimentiRepository
    {
        public FinanceAppContext Context { get; set; }

        public MovimentiRepository(FinanceAppContext context)
        {
            Context = context;
        }
    }
}
