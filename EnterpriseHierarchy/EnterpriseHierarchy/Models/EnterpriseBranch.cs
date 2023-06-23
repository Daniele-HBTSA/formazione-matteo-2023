namespace EnterpriseHierarchy.Models
{
    public class EnterpriseBranch
    {
        public EnterpriseDTO? ChildEnterprise { get; set; }
        public List<EnterpriseBranch>? ChildEnterpriseChildren { get; set; }

        public EnterpriseBranch(EnterpriseDTO? enterpriseChild)
        {
            if (ChildEnterprise != null)
            {
                ChildEnterprise = enterpriseChild;
                ChildEnterpriseChildren = new List<EnterpriseBranch>();

            }
        }
    }
}
