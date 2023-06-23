namespace EnterpriseHierarchy.Models
{
    public class EnterpriseTree
    {
        public EnterpriseDTO EnterpriseFather { get; set; } = null!;
        public List<EnterpriseBranch>? Children { get; set; }

        public EnterpriseTree(EnterpriseDTO enterpriseFather)
        {
            EnterpriseFather = enterpriseFather;
            Children = new List<EnterpriseBranch>();
        }
    }
}
