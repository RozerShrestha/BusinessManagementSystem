using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TattooAppointmentSystem.Models
{
    public class Menu:BaseEntity
    {
        public int Id { get; set; }
        //public int MenuId { get; set; }
        public int Parent { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Sort must be 1 or a positive number.")]

        public int Sort { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string Icon { get; set; }

        [ValidateNever]
        [NotMapped]
        public Multiselect Multiselect { get; set; }
        
        [ValidateNever]
        [NotMapped]
        public string Roles { get; set; }
        [ValidateNever]
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
    }

    public class MenuEntityConfiguration : IEntityTypeConfiguration<Menu>

    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(x => x.Name).HasColumnType("varchar(50)");
            builder.Property(x => x.Url).HasColumnType("varchar(255)");
            builder.Property(x => x.Icon).HasColumnType("varchar(150)");

        }
    }
}

