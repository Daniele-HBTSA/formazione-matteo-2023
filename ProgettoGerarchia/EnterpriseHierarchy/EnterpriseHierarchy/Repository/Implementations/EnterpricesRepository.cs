using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHierarchy.Repository.Implementations
{
    public class EnterpricesRepository : IEnterpricesRepository
    {
        public EnterpriseHierarchyContext DBContext;

        public EnterpricesRepository(EnterpriseHierarchyContext context)
        {
            DBContext = context;
        }

        public async Task<List<ENTERPRISES>> GetAllFromDB()
        {
            return await DBContext.ENTERPRISES.ToListAsync();
        }
    }
}
