using BusinessManagementSystem.Helper;
using BusinessManagementSystem.Migrations;
using BusinessManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace BusinessManagementSystem.Dto
{
    public class PaymentTipSettlementDto
    {
        public List<PaymentSettlementDto> PaymentSettlements { get; set;}
        public List<TipSettlementDto> TipSettlements { get; set; }
        public List<AdvancePaymentDto> AdvancePaymentSettlements { get; set; }
        public int UserId { get; set; }
        public string? ArtistName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPayments { get; set; }
        public double TotalTips { get; set; }
        public double TotalAdvancePayments { get; set; }
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
        public string TipAssignedUser { get; set; }
        public double TipAmount { get; set; }
        public double TipAmountForUser { get; set; }
        public bool TipSettlement { get; set; }
        public string Status { get; set; } //Appointment Status

    }
    public class AdvancePaymentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public double Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string Reason { get; set; }
        public DateOnly PaidDate { get; set; }
        public bool Status { get; set; }
    }

}
