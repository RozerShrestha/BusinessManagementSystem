namespace BusinessManagementSystem.Dto
{
    public class DueCostRequestDto
    {
        public bool IsForeigner { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Subcategory { get; set; } = string.Empty;
        public double TotalHours { get; set; }
        public int Deposit { get; set; }
        public int Discount { get; set; }
        public double DiscountInHour { get; set; }
        public double PaidAmount { get; set; }
    }
}
