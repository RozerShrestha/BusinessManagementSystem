using BusinessManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusinessManagementSystem.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public Guid guid { get; set; }
        public string UserName { get; set; }
        public string ReferalName { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Category { get; set; }
        public double TotalHours { get; set; }
        public double Deposit { get; set; }
        public double Discount { get; set; }
        public double DiscountInHour { get; set; }
        public double TotalCost { get; set; }
        public string Status { get; set; }
        public string? TattooDesign { get; set; }
        public string? Placement { get; set; }
        public string? InkColorPreferance { get; set; }
        public string Allergies { get; set; }
        public string MedicalConditions { get; set; }
        public string PainToleranceLevel { get; set; }
        public int SessionNumber { get; set; }
        public bool ConsentFormSigned { get; set; }
        public bool FollowUpRequired { get; set; }
    }
}
