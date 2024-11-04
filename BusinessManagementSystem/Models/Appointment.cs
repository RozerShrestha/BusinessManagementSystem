using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessManagementSystem.Models
{
    public class Appointment:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public Guid guid { get; set; }
        [Required]
        public string ClientName { get; set; }
        [Required]
        public string ClientPhoneNumber { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Referal Referal { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public double EstimatedHours { get; set; }
        [Required]
        public double Deposit { get; set; }
        public double Fee { get; set; }
        public double Discount { get; set; }
        [Required]
        public string Status { get; set; }
        public string TattooDesign { get; set; }
        public string Placement { get; set; }
        public string InkColorPreferance { get; set; }
        public string ArtistPreferance { get; set; }
        public string Allergies { get; set; }
        public string MedicalConditions { get; set; }
        public string PainToleranceLevel { get; set; }
        public int SessionNumber { get; set; }
        public bool ConsentFormSigned { get; set; }
        public bool FollowUpRequired { get; set; }

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
