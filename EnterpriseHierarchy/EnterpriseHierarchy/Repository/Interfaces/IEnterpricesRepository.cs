using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Repository.Interfaces
{
    public interface IEnterpricesRepository
    {
        public Task<EnterpriseDTO> GetEnterpriseDTOByID(int entID);
        public Task<EnterpriseDTO> GetEnterpriseDTOByCode(string entCode);
    }
}
