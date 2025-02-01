using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Org.BouncyCastle.Utilities.IO.Pem;
using System.ComponentModel;
using BusinessManagementSystem.Helper;

namespace BusinessManagementSystem.Models
{
    public class AdvancePayment:BaseEntity
    {
        public int Id { get; set; }
        public Guid guid { get; set; }
        [DisplayName("Artist Name")]
        public int UserId { get; set; }
        [NotMapped]
        [ValidateNever]
        public string FullName { get; set; }
        public double Amount { get; set; }
        [RequiredIfValueMatchAttribute(nameof(Status), true, "If Status is Pay, PaymentMethod is required")]
        public string? PaymentMethod { get; set; }
        public string Reason { get; set; }
        public DateOnly PaidDate { get; set; }
        public bool Status { get; set; }
        [ValidateNever]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        
    }
    public class AdvancePaymentEntityConfiguration : IEntityTypeConfiguration<AdvancePayment>
    {
        public void Configure(EntityTypeBuilder<AdvancePayment> builder)
        {
            builder.Property(x => x.PaymentMethod).HasColumnType("varchar(100)");
            builder.Property(x => x.Reason).HasColumnType("varchar(500)");
        }
    }
}
