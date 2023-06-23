using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;
using EnterpriseHierarchy.Services.Interfaces;

namespace EnterpriseHierarchy.Services.Implementations
{
    public class TreeService : ITreeService
    { 
        public IEnterpricesRepository Repo { get; set; }

        public TreeService(IEnterpricesRepository enterpricesRepository)
        {
            Repo = enterpricesRepository;
        }

        public async Task<EnterpriseTree> CreateTreeStruct(int FatherID)
        {
            EnterpriseDTO FatherDTO = await Repo.GetEnterpriseDTOByID(FatherID);
            EnterpriseTree newTree = new EnterpriseTree(FatherDTO);

            if (FatherDTO.ParentIDs != null)
            {
                foreach (int childID in FatherDTO.ParentIDs)
                {
                    EnterpriseDTO child = await newChild(childID);
                    EnterpriseBranch branch = await NewBranch(child);
                    newTree.Children!.Add(branch);
                }
            }

            return newTree;
        }

        public async Task<EnterpriseBranch> NewBranch(EnterpriseDTO ChildDTO)
        {
            EnterpriseBranch branch = new EnterpriseBranch(ChildDTO);
            branch.ChildEnterpriseChildren = new List<EnterpriseBranch>();
            
            if (ChildDTO.ParentIDs != null)
            {
                foreach (int childID in ChildDTO.ParentIDs)
                {
                    EnterpriseDTO childOfChild = await newChild(childID);

                    while (childOfChild.ParentIDs != null && childOfChild.ParentIDs.Any())
                    {
                        EnterpriseBranch newBranch = await NewBranch(childOfChild);
                        branch.ChildEnterpriseChildren.Add(newBranch);
                        childOfChild = await newChild(childID); 
                    }
                }
            }

            return branch;
        }

        public async Task<EnterpriseDTO> newChild(int childID)
        {
            return await Repo.GetEnterpriseDTOByID(childID);
        }

    }
}
