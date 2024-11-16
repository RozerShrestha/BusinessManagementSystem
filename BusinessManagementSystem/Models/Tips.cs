using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessManagementSystem.Models
{
    public class Tip:BaseEntity
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public double TipAmount { get; set; }
        public double TipAmountForUsers { get; set; }
        public int TipAssignedToUser { get; set; }
        
        [ForeignKey("AppointmentId")]
        [ValidateNever]
        public virtual Appointment Appointment { get; set; }
        public bool TipSettlement { get; set; }

    }
    public class TipEntityConfiguration : IEntityTypeConfiguration<Tip>
    {
        public void Configure(EntityTypeBuilder<Tip> builder)
        {
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}
