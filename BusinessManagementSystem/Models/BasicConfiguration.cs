using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BusinessManagementSystem.Models
{
    public class BasicConfiguration:BaseEntity
    {
        public int Id { get; set; }

        [DisplayName("Email Alias")]
        [Required]
        public required string EmailAlias { get; set; }
        [DisplayName("Sender Email Address")]
        [Required]
        public required string Email { get; set; }
        public required string Password { get; set; }
        [DisplayName("Host Name")]
        [Required]
        public required string HostName { get; set; }
        [DisplayName("Port")]
        [Required]
        public required int Port { get; set; }
        [DisplayName("Application Title")]
        [Required]
        public required string ApplicationTitle { get; set; }

        [DisplayName("Employer Name")]
        [Required]
        public required string EmployerName { get; set; }

        [DisplayName("Employer Email Address")]
        [Required]
        public required string EmployerEmailAddress { get; set; }

        [DisplayName("Employer Address")]
        [Required]
        public required string EmployerAddress { get; set; }
        [DisplayName("Tattoo Price")]
        [Required]
        public double TattooPrice { get; set; }
        [DisplayName("Piercing Price")]
        [Required]
        public double PiercingPrice { get; set; }
        [DisplayName("DeadLock Price")]
        [Required]
        public double DreadLockPrice { get; set; }
        [DisplayName("New User Template")]
        public string NewUserEmailTemplate { get; set; }
        [DisplayName("New Appointment Template Client")]
        public string NewAppointmentTemplateClient { get; set; }
        [DisplayName("New Appointment Template Artist")]
        public string NewAppointmentTemplateArtist { get; set; }
        [DisplayName("Update Appointment Template Client")]
        public string AppointmentUpdateTemplateClient { get; set; }
        [DisplayName("Update Appointment Template Artist")]
        public string AppointmentUpdateTemplateArtist { get; set; }
        [DisplayName("Payment Settlement Template Artist")]
        public string PaymentSettlementTemplateArtist { get; set; }
        [DisplayName("Appointment Complete Client")]
        public string AppointmentCompletedClient { get; set; }
        [DisplayName("Appointment Complete Artist")]
        public string AppointmentCompletedArtist { get; set; }
        [DisplayName("Advance Payment Artist Template")]
        public string AdvancePaymentArtistTemplate { get; set; }
        [DisplayName("Advance Payment Superadmin Template")]
        public string AdvancePaymentSuperadminTemplate { get; set; }



    }
    public class BasicConfigurationEntityConfiguration : IEntityTypeConfiguration<BasicConfiguration>
    {
        public void Configure(EntityTypeBuilder<BasicConfiguration> builder)
        {
            builder.Property(x => x.EmployerName).HasColumnType("varchar(100)");
            builder.Property(x => x.Email).HasColumnType("varchar(Max)");
            builder.Property(x => x.EmailAlias).HasColumnType("varchar(100)");
            builder.Property(x => x.HostName).HasColumnType("varchar(100)");
            builder.Property(x => x.EmployerEmailAddress).HasColumnType("varchar(100)");
            builder.Property(x => x.EmployerAddress).HasColumnType("varchar(100)");
            builder.Property(x => x.Password).HasColumnType("varchar(250)");
            builder.Property(x => x.ApplicationTitle).HasColumnType("varchar(250)");
            builder.Property(x => x.CreatedBy).HasColumnType("varchar(150)");
            builder.Property(x => x.UpdatedBy).HasColumnType("varchar(150)");

        }
    }
}
