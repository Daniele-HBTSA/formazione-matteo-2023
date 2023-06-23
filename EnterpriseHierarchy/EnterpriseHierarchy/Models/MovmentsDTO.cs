namespace EnterpriseHierarchy.Models
{
    public class MovmentsDTO
    {
        public int IdMovment { get; set; }
        public string? MovmentName { get; set; }
        public int? Cost { get; set; }
        public int? Income { get; set; }
        public int EnterpriseID { get; set; }
    }
}
