using FinanceApp.Context;

namespace FinanceApp.Repository.InterfacesImpl
{
    public class AziendeRepository
    {
        public FinanceAppContext Context { get; set; }

        public AziendeRepository(FinanceAppContext context)
        {
            Context = context;
        }
    }
}
