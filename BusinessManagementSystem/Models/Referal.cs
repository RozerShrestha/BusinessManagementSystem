using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystem.Models
{
    public class Referal:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Status { get; set; }
    }
    public class ReferalEntityConfiguration : IEntityTypeConfiguration<Referal>
    {
        public void Configure(EntityTypeBuilder<Referal> builder)
        {
            builder.Property(x => x.FullName).HasColumnType("varchar(255)");
        }
    }
}
