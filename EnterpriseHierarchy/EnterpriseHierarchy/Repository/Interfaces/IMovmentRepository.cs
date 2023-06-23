using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Repository.Interfaces
{
    public interface IMovmentRepository
    {
        public Task<MovmentsDTO> GetMovmentsDTOByID(int id);
        public Task<MovmentsDTO> GetMovmentsDTOByEnterpriseID(int entId);
    }
}
