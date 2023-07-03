using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Repository.Interfaces
{
    public interface IEnterpricesRepository
    {
        public Task<List<ENTERPRISES>> GetAllFromDB();
        public Task<List<ENTERPRISES>> AddNewEnterpriseOnDB(EnterpriseTree newChild, int fatherId);
    }
}
