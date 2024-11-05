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
        [Required]
        public Guid guid { get; set; }
        public int UserId { get; set; }
        public int ReferalId { get; set; }
        [Required]
        [DisplayName("Client Name")]
        public string ClientName { get; set; }
        [Required]
        [DisplayName("Client Phone Number")]
        public string ClientPhoneNumber { get; set; }
        [Required]
        [DisplayName("Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [DisplayName("Estimated Hours")]
        public double EstimatedHours { get; set; }
        [Required]
        public double Deposit { get; set; }
        public double Fee { get; set; }
        public double Discount { get; set; }
        [Required]
        public string Status { get; set; }
        [DisplayName("Tattoo Design")]
        public string? TattooDesign { get; set; }
        public string? Placement { get; set; }
        [DisplayName("Ink Color Preferance")]
        public string? InkColorPreferance { get; set; }
        [Required]
        [DisplayName("Artist Preference")]
        public string ArtistPreferance { get; set; }
        [Required]
        public string Allergies { get; set; }
        [Required]
        [DisplayName("Medical Conditions")]
        public string MedicalConditions { get; set; }
        [DisplayName("Pain Tolerance Level")]
        public string PainToleranceLevel { get; set; }
        [DisplayName("Session Number")]
        public int SessionNumber { get; set; }
        [Required]
        [DisplayName("Consent Form Sign")]
        public bool ConsentFormSigned { get; set; }
        [DisplayName("Followup Required")]
        public bool FollowUpRequired { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Referal Referal { get; set; }
        //public Appointment(int appointmentId, User user, Referal referal)
        //{
        //    Id = appointmentId;
        //    UserId=user.Id;
        //    ReferalId=referal.Id;
        //}

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
            builder.Property(x => x.ArtistPreferance).HasColumnType("varchar(150)");
            builder.Property(x => x.Allergies).HasColumnType("varchar(500)");
            builder.Property(x => x.MedicalConditions).HasColumnType("varchar(500)");
            builder.Property(x => x.PainToleranceLevel).HasColumnType("varchar(150)");
        }
    }
}
