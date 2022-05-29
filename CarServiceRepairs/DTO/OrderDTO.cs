namespace CarServiceRepairs.DTO
{
    public class OrderDTO
    {
        public string Breakage { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime WarrantyEnd { get; set; }
        public decimal Price { get; set; }
        public int AutoId { get; set; }
        public int WorkerId { get; set; }
        public int DetailId { get; set; }
        public int Amount { get; set; }
    }
}
