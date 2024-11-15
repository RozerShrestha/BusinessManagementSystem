using BusinessManagementSystem.Helper;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessManagementSystem.Models
{
    public class Appointment:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid guid { get; set; }
        [DisplayName("Artist Preferance")]
        public int UserId { get; set; }
        [DisplayName("Referal")]
        public int ReferalId { get; set; }
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
        //we need required if Status is something
        [DisplayName("Total Hours *")]
        public double TotalHours { get; set; }
        [Required]
        [Range(1000, int.MaxValue, ErrorMessage ="Deposit amount should be more than equal to 1000")]
        [DisplayName("Deposit *")]
        public double Deposit { get; set; }
        [DisplayName("Discount")]
        public double Discount { get; set; }
        [DisplayName("Discount In Hour")]
        public double DiscountInHour { get; set; }
        [DisplayName("Total Cost")]
        public double TotalCost { get; set; }
        [Required]
        [DisplayName("Status *")]
        public string Status { get; set; }
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
        [RequiredIf("ConsentFormSigned","False","Concent Form should be Yes")]
        [DisplayName("Consent Form Sign")]
        public bool ConsentFormSigned { get; set; }
        [DisplayName("Followup Required")]
        public bool FollowUpRequired { get; set; }
        [DisplayName("Is Foreigner")]
        public bool IsForeigner { get; set; }
        [DisplayName("Tips if Available")]
        public double? TipAmount { get; set; }

        //[RequiredIf("Status","Completed")]
        [ValidateNever]
        public ICollection<Tip> Tips { get; set; }

        //[JsonIgnore]
        [ValidateNever]
        [ForeignKey("UserId")]
        
        public User User { get; set; }
        //[JsonIgnore]
        [ForeignKey("ReferalId")]
        [ValidateNever]
        public Referal Referal { get; set; }

    }

    public class AppointmentEntityConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(x => x.ClientName).HasColumnType("varchar(255)");
            builder.Property(x => x.ClientPhoneNumber).HasColumnType("varchar(20)");
            builder.Property(x => x.Category).HasColumnType("varchar(50)");
            builder.Property(x => x.Status).HasColumnType("varchar(100)");
            builder.Property(x => x.TattooDesign).HasColumnType("varchar(500)");
            builder.Property(x => x.Placement).HasColumnType("varchar(150)");
            builder.Property(x => x.InkColorPreferance).HasColumnType("varchar(150)");
            builder.Property(x => x.Allergies).HasColumnType("varchar(500)");
            builder.Property(x => x.MedicalConditions).HasColumnType("varchar(500)");
            builder.Property(x => x.PainToleranceLevel).HasColumnType("varchar(150)");
        }
    }
}
