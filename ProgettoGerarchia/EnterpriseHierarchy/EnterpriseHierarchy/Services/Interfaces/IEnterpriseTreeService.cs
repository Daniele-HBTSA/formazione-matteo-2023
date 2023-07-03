using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Services.Interfaces
{
    public interface IEnterpriseTreeService
    {
        public Task<List<EnterpriseTree>> GetEnterpriseData();
        public Task<List<EnterpriseTree>> AddNewEnterprise(EnterpriseTree newChild, int fatherId);
        public int SumEveryEnterpriseTreeBalances(List<EnterpriseTree> tree);
        public int SumSpecificEnterpriseTreeBalances(List<EnterpriseTree> tree, int fatherID);
        public List<EnterpriseTree> SelectSpecificEnterpriseTree(List<EnterpriseTree> tree, int fatherID);
        public List<EnterpriseTree> SelectSingleEnterprise(List<EnterpriseTree> tree, int enterpriseID);
    }
}
