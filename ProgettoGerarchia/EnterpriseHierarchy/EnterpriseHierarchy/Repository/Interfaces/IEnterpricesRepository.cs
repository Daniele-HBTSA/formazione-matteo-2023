using EnterpriseHierarchy.Context;

namespace EnterpriseHierarchy.Repository.Interfaces
{
    public interface IEnterpricesRepository
    {
        public Task<List<ENTERPRISES>> GetAllFromDB();
    }
}
