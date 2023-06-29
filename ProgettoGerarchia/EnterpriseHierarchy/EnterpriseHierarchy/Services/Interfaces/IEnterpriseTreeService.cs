using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Services.Interfaces
{
    public interface IEnterpriseTreeService
    {
        public Task<List<EnterpriseTree>> GetEnterpriseData();
        public int SumEveryEnterpriseTreeBalances(List<EnterpriseTree> tree);
        public int SumSpecificEnterpriseTreeBalances(List<EnterpriseTree> tree, int fatherID);
        public List<EnterpriseTree> SelectSpecificEnterpriseTree(List<EnterpriseTree> tree, int fatherID);
    }
}
