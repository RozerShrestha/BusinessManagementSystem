namespace BusinessManagementSystem.Dto
{
    public class DueCostResponseDto
    {
        public double DueAmount { get; set; }
        public double TotalCost { get; set; }
        public string CostDescription { get; set; } = string.Empty;
    }
}
