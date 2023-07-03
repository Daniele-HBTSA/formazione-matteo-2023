using EnterpriseHierarchy.Context;
using EnterpriseHierarchy.Models;
using EnterpriseHierarchy.Repository.Interfaces;
using EnterpriseHierarchy.Services.Interfaces;
using System.Collections.Generic;

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

        public async Task<List<EnterpriseTree>> AddNewEnterprise(EnterpriseTree newChild, int fatherId)
        {
            List<ENTERPRISES> DBEntities = await EnterpriseRepo.GetAllFromDB();

            if (DBEntities.FirstOrDefault(element => element.ENT_CODE.Equals(newChild.Code)) != null)
            {
                throw new Exception("Code already exists");
            }

            return CreateTree(await EnterpriseRepo.AddNewEnterpriseOnDB(newChild, fatherId));
        }

        private List<EnterpriseTree> CreateTree(List<ENTERPRISES> listaDB, int? parentID = null) 
        {
            List<EnterpriseTree> Tree = new List<EnterpriseTree>();

            List<ENTERPRISES> parent = listaDB.Where(element => element.ENT_PARENT_ID.Equals(parentID)).ToList();

            foreach (ENTERPRISES item in parent)
            {
                Tree.Add(new EnterpriseTree()
                {
                    Id = item.ID_ENTERPRISE,
                    Code = item.ENT_CODE,
                    Balance = item.ENT_BALACE,
                    Children = CreateTree(listaDB, item.ID_ENTERPRISE)
                }); ; 
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
        public List<EnterpriseTree> SelectSingleEnterprise(List<EnterpriseTree> tree, int enterpriseID) 
        {
            foreach(EnterpriseTree father in tree)
            {
                if(father.Id.Equals(enterpriseID))
                {
                    father.Selected = !father.Selected;
                    return tree;

                } else
                {
                    foreach(EnterpriseTree child in father.Children)
                    {
                        if (child.Id.Equals(enterpriseID))
                        {
                            child.Selected = !child.Selected;
                            return tree;

                        } else
                        {
                           SelectSingleEnterprise(father.Children, enterpriseID);
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
            node.Selected = !node.Selected;

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
