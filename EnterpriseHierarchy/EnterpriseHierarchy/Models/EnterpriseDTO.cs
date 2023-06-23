namespace EnterpriseHierarchy.Models
{
    public class EnterpriseDTO
    {
        public int IdEnterprise { get; set; }
        public string EnterpriseCode { get; set; } = null!;
        public string? EnterpriseName { get; set; }
        public string? EnterpriseAddress { get; set; }
        public int[] ParentIDs { get; set; } = null!;
    }
}
