using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public async Task<List<ENTERPRISES>> AddNewEnterpriseOnDB(EnterpriseTree newChild, int fatherId)
        {
            ENTERPRISES newEnterprise = new ENTERPRISES()
            {
                ENT_CODE = newChild.Code,
                ENT_BALACE = newChild.Balance,
                ENT_PARENT_ID = fatherId
            };

            try
            {
                await DBContext.ENTERPRISES.AddAsync(newEnterprise);
                await DBContext.SaveChangesAsync();

                return await DBContext.ENTERPRISES.ToListAsync();

            } catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
