using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;
using EnterpriseHierarchy.Services.Interfaces;

namespace EnterpriseHierarchy.Services.Implementations
{
    public class EnterpriseTreeService : IEnterpriseTreeService
    {
        public IEnterpricesRepository EnterpriseRepo { get; set; }

        public EnterpriseTreeService(IEnterpricesRepository enterpricesRepository)
        {
            EnterpriseRepo = enterpricesRepository;
        }

        public async Task<List<EnterpriseTree>> GetEnterpriseData()
        {
            return CreateTree(await EnterpriseRepo.GetAllFromDB());
        }

        private List<EnterpriseTree> CreateTree(List<ENTERPRISES> listaDB, int? parentID = null) 
        {
            List<EnterpriseTree> Tree = new List<EnterpriseTree>();

            List<ENTERPRISES> parent = listaDB.Where(element => element.ENT_PARENT_ID.Equals(parentID)).ToList();

            foreach (ENTERPRISES item in parent)
            {
                Tree.Add(new EnterpriseTree()
                {
                    Code = item.ENT_CODE,
                    Balance = item.ENT_BALACE,
                    Children = CreateTree(listaDB, item.ID_ENTERPRISE)
                }); 
            }

            return Tree;
        }

        //Metodo che effettua la somma di tutti i saldi di ogni azienda
        public int SumEveryEnterpriseTreeBalances(List<EnterpriseTree> tree)
        {
            int total = 0;

            foreach (EnterpriseTree parent in tree)
            {
                total += parent.Balance;

                if(parent.Children.Any())
                {
                    foreach (EnterpriseTree children in parent.Children)
                    {
                        total += children.Balance + SumEveryEnterpriseTreeBalances(children.Children);
                    }
                }
            }
            return total;
        }

        //Metodo che effettua la somma di tutti i saldi di una specifica azienda
        public int SumSpecificEnterpriseTreeBalances(List<EnterpriseTree> tree, int fatherID)
        {
            List<EnterpriseTree> specificTree = tree.Where(element => element.Id.Equals(fatherID)).ToList();
            return SumEveryEnterpriseTreeBalances(specificTree);
        }

        //Metodo che cambia lo stato Selected ad una specifica azienda
        public List<EnterpriseTree> SelectSpecificEnterprise(List<EnterpriseTree> tree, int enterpriseID) 
        {
            foreach(EnterpriseTree father in tree)
            {
                if(father.Id.Equals(enterpriseID))
                {
                    //father.Selected = !father.Selected;
                    father.Selected = true;
                    return tree;

                } else
                {
                    foreach(EnterpriseTree child in father.Children)
                    {
                        if (child.Id.Equals(enterpriseID))
                        {
                            //child.Selected = !child.Selected;
                            child.Selected = true;
                            return tree;

                        } else
                        {
                            SelectSpecificEnterprise(father.Children, enterpriseID);
                        }
                    }
                }
            }
            return tree;
        }

        //Metodi che cambia lo stato Selected ad uno specifico tree
        public List<EnterpriseTree> SelectSpecificEnterpriseTree(List<EnterpriseTree> tree, int fatherID)
        {
            foreach (EnterpriseTree father in tree)
            {
                if (father.Id.Equals(fatherID))
                {
                    SelectEveryEnterpriseInTree(father);
                    break;
                }
            }

            return tree;
        }

        private void SelectEveryEnterpriseInTree(EnterpriseTree node)
        {
            node.Selected = true;

            if (node.Children != null)
            {
                foreach (EnterpriseTree child in node.Children)
                {
                    SelectEveryEnterpriseInTree(child);
                }
            }
        }

        //          Mio bordello
        //public List<EnterpriseTree> SelectSpecificEnterpriseTree(List<EnterpriseTree> tree, int fatherID)
        //{
        //    foreach (EnterpriseTree father in tree)
        //    {
        //        if (father.Id.Equals(fatherID))   //Il problema è qua, perchè non si aggiorna fatherID - per qualche motivo.
        //        {
        //            father.Selected = true;

        //            if (father.Children.Any())
        //            {
        //                foreach (EnterpriseTree child in father.Children)
        //                {
        //                    child.Selected = true;

        //                    if(child.Children.Any())
        //                    {
        //                        SelectSpecificEnterprise(child.Children, child.Id);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tree;
        //}


    }
}
