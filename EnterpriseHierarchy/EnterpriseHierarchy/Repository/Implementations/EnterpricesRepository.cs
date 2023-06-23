using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;

namespace EnterpriseHierarchy.Repository.Implementations
{
    public class EnterpricesRepository : IEnterpricesRepository
    {
        public EnterpriseHierarchyContext DBContext;

        public EnterpricesRepository(EnterpriseHierarchyContext context)
        {
            this.DBContext = context;
        }

        //Entities getters
        public async Task<ENTERPRISES> GetEnteripriseEntityByID(int entID)
        {
            ENTERPRISES? Enterprise = await DBContext.ENTERPRISES.Where(element => element.ID_ENTERPRISE == entID).FirstOrDefaultAsync();

            if (Enterprise == null)
            {
                throw new Exception("Id not found");
            }
            return Enterprise;
        }

        public async Task<ENTERPRISES> GetEnteripriseEntityByCode(string entCode)
        {
            ENTERPRISES? Enterprise = await DBContext.ENTERPRISES.Where(element => element.ENT_CODE == entCode).FirstOrDefaultAsync();
            if (Enterprise == null)
            {
                throw new Exception("Code not found");
            }
            return Enterprise;
        }

        //DTOs getters
        public async Task<EnterpriseDTO> GetEnterpriseDTOByID(int entID)
        {
            ENTERPRISES Enterprise = await GetEnteripriseEntityByID(entID);
            EnterpriseDTO EnterpriseDTO = new EnterpriseDTO()
            {
                IdEnterprise = Enterprise.ID_ENTERPRISE,
                EnterpriseCode = Enterprise.ENT_CODE,
                EnterpriseName = Enterprise.ENT_NAME,
                EnterpriseAddress = Enterprise.ENT_ADDRESS,
                ParentIDs = Enterprise.ENT_PARENT_IDs
            };

            return EnterpriseDTO;
        }

        public async Task<EnterpriseDTO> GetEnterpriseDTOByCode(string entCode)
        {
            ENTERPRISES Enterprise = await GetEnteripriseEntityByCode(entCode);
            EnterpriseDTO EnterpriseDTO = new EnterpriseDTO()
            {
                IdEnterprise = Enterprise.ID_ENTERPRISE,
                EnterpriseCode = Enterprise.ENT_CODE,
                EnterpriseName = Enterprise.ENT_NAME,
                EnterpriseAddress = Enterprise.ENT_ADDRESS,
                ParentIDs = Enterprise.ENT_PARENT_IDs
            };

            return EnterpriseDTO;
        }
    }
}
