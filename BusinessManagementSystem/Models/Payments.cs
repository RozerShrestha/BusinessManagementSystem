using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManagementSystem.Models
{
    public class Payment : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AppointmentId { get; set; }
        public double Deposit { get; set; }
        public double Discount { get; set; }
        public double DiscountInHour { get; set; }
        public double TotalCost { get; set; }
        public double TipAmount { get; set; }
        public double PaymentToStudio { get; set; }
        public double PaymentToArtist { get; set; }
        public string PaymentMethod { get; set; }
        public bool PaymentSettlement { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(AppointmentId))]
        public Appointment Appointment { get; set; }

    }
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.PaymentMethod).HasColumnType("varchar(100)");
        }
    }
}
