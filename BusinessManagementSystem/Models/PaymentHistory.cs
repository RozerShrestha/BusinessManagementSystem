using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessManagementSystem.Models
{
    public class PaymentHistory : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public double TotalPayment { get; set; }
        public double TotalTips { get; set; }
        public double GrandTotal { get; set; }
        public string PaidStatus { get; set; }
        public DateOnly PaymentFrom { get; set; }
        public DateOnly PaymentTo { get; set; }
        [ValidateNever]
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
    public class PaymentHistoryEntityConfiguration : IEntityTypeConfiguration<PaymentHistory>
    {
        public void Configure(EntityTypeBuilder<PaymentHistory> builder)
        {
            builder.HasIndex(x => new { x.PaymentFrom, x.PaymentTo}).IsUnique();
            builder.Property(x => x.PaidStatus).HasColumnType("varchar(100)");
        }
    }
}
   