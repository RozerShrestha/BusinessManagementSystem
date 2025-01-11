using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessManagementSystem.Dto
{
    public class PaymentTipSettlementDto
    {
        public List<PaymentSettlementDto> PaymentSettlements { get; set;}
        public List<TipSettlementDto> TipSettlements { get; set; }
        public int UserId { get; set; }
        public string? ArtistName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPayments { get; set; }
        public double TotalTips { get; set; }
        public double GrandTotal { get; set; }
    }
    public class PaymentSettlementDto
    {
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public int PaymentId { get; set; } //it is used to udapte payment settlement
        public DateTime AppointmentDate { get; set; }
        public DateTime? PaymentUpdatedDate { get; set; }
        public string ClientName { get; set; }
        public string ArtistName { get; set; }
        public double TotalCost { get; set; }
        public string PaymentMethod { get; set; }
        public bool PaymentSettlement { get; set; }
        public string Status { get; set; }
        public double PaymentToStudio { get; set; }
        public double PaymentToArtist { get; set; }
         
    }
    public class TipSettlementDto
    {
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public int TipId { get; set; } //it is used to update tip settlement
        public DateTime AppointmentDate { get; set; }
        public DateTime? TipCreatedDate { get; set; }
        public string ClientName { get; set; }
        public string ArtistName { get; set; }
        public double TipAmount { get; set; }
        public double TipAmountForUser { get; set; }
        public bool TipSettlement { get; set; }
        public string Status { get; set; } //Appointment Status

    }
}
