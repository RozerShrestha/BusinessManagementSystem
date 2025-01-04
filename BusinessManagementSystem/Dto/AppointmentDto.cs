using BusinessManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BusinessManagementSystem.Helper;

namespace BusinessManagementSystem.Dto
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid guid { get; set; }
        [DisplayName("Artist Preferance")]
        public int UserId { get; set; }
        public string? ArtistAssigned { get; set; }
        [DisplayName("Referal")]
        public int ReferalId { get; set; }
        public string? ReferalFullName { get; set; }
        [Required]
        [DisplayName("Client Name *")]
        public string ClientName { get; set; }
        [Required]
        [DisplayName("Client Phone Number *")]
        public string ClientPhoneNumber { get; set; }
        [Required]
        [DisplayName("Appointment Date *")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [DisplayName("Category *")]
        public string Category { get; set; }
        [DisplayName("Tattoo Design")]
        public string? TattooDesign { get; set; }
        [DisplayName("Placement")]
        public string? Placement { get; set; }
        [DisplayName("Ink Color Preferance")]
        public string? InkColorPreferance { get; set; }
        [Required]
        [DisplayName("Allergies *")]
        public string Allergies { get; set; }
        [Required]
        [DisplayName("Medical Conditions *")]
        public string MedicalConditions { get; set; }
        [DisplayName("Pain Tolerance Level *")]
        public string PainToleranceLevel { get; set; }
        [DisplayName("Session Number *")]
        public int SessionNumber { get; set; }
        [RequiredIf(nameof(ConsentFormSigned), "False", "Concent Form should be Yes")]
        [DisplayName("Consent Form Sign")]
        public bool ConsentFormSigned { get; set; }
        [DisplayName("Followup Required")]
        public bool FollowUpRequired { get; set; }
        [DisplayName("Is Foreigner")]
        public bool IsForeigner { get; set; }
        public string? Outlet { get; set; }
        [DisplayName("Total Hours *")]
        public double TotalHours { get; set; }
        [Required]
        [DisplayName("Status *")]
        public string Status { get; set; }
        [Required]
        [Range(1000, int.MaxValue, ErrorMessage = "Deposit amount should be more than equal to 1000")]
        [DisplayName("Deposit *")]
        public double Deposit { get; set; }
        [DisplayName("Discount")]
        public double Discount { get; set; }
        [DisplayName("Discount In Hour")]
        public double DiscountInHour { get; set; }
        [DisplayName("Total Cost")]
        public double TotalCost { get; set; }
        [DisplayName("Tips if Available")]
        public double TipAmount { get; set; }
        [DisplayName("Payment Method")]
        [RequiredIfValueMatchAttribute(nameof(Status),"Completed","If Status is Completed, PaymentMethod is required")] 
        public string? PaymentMethod { get; set; }
        public int AppointmentCreatedId { get; set; }
    }
}
