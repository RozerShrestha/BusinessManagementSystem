using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Dto
{
    public class PaymentDto:BaseEntity
    {
        public int PaymentId { get; set; }
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public string ArtistName { get; set; }
        public double Deposit { get; set; }
        public double Discount { get; set; }
        public double DiscountInHour { get; set; }
        public double TotalCost { get; set; }
        public double PaymentToStudio { get; set; }
        public double PaymentToArtist { get; set; }
        public string PaymentMethod { get; set; }
        public bool PaymentSettlement { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
