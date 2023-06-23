using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;

namespace EnterpriseHierarchy.Repository.Implementations
{
    public class MovmentsRepository : IMovmentRepository
    {
        public EnterpriseHierarchyContext context;

        public MovmentsRepository(EnterpriseHierarchyContext context)
        {
            this.context = context;
        }

        //Entities getters
        public async Task<List<ENT_MOVMENTS>> GetMovmentsEntityByID(int entID)
        {

        }

        //DTOs getters
        public Task<MovmentsDTO> GetMovmentsDTOByID(int id)
        {

        }

        public Task<MovmentsDTO> GetMovmentsDTOByEnterpriseID(int entId)
        {

        }
    }
}
