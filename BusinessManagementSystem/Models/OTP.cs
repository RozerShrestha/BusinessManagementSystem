using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace TattooAppointmentSystem.Models
{
    public class OTP:BaseEntity
    {
        public int Id { get; set; }
        public int  UserId { get; set; }
        public string Email { get; set; }
        public int OtpCode { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }

    }

    public class OTPEntityConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.Property(x => x.Email).HasColumnType("varchar(500)");
        }
    }
}

