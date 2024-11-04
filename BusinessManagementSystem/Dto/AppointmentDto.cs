using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Category { get; set; }
        public double EstimatedHours { get; set; }
        public double Deposit { get; set; }
        public double Fee { get; set; }
        public double Discount { get; set; }
        public string Status { get; set; }
    }
}
