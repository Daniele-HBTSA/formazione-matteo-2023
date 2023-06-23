using EnterpriseHierarchy.Models;

namespace EnterpriseHierarchy.Services.Interfaces
{
    public interface ITreeService
    {
        public Task<EnterpriseTree> CreateTreeStruct(int FatherID);
    }
}
