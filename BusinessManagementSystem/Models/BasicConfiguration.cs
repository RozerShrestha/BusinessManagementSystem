using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManagementSystem.Models
{
    public class BasicConfiguration:BaseEntity
    {
        public int Id { get; set; }
        [DisplayName("Email Alias")]
        [Required]
        public string EmailAlias { get; set; }
        [DisplayName("Sender Email Address")]
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Host Name")]
        [Required]
        public string HostName { get; set; }
        [DisplayName("Port")]
        [Required]
        public int Port { get; set; }
        [DisplayName("Application Title")]
        [Required]
        public string ApplicationTitle { get; set; }
        [DisplayName("Insurance Company Name")]
        [Required]
        public string InsuranceCompanyName { get; set; }
        public bool ShowProductWalkThrough { get; set; }
        [DisplayName("Insurance Policy Doc1")]
        public string? InsurancePolicyDoc1 { get; set; }
        [DisplayName("Insurance Policy Doc2")]
        public string? InsurancePolicyDoc2 { get; set; }
        [DisplayName("Insurance Policy Doc3")]
        public string? InsurancePolicyDoc3 { get; set; }
        [DisplayName("Insurance Policy Doc4")]
        public string? InsurancePolicyDoc4 { get; set; }
        [DisplayName("Insurance Policy Doc5")]
        public string? InsurancePolicyDoc5 { get; set; }
        [DisplayName("Any Other Doc Upload")]
        public string? OtherDocLink { get; set; }


        public string? InsurancePolicyDoc1FileName { get; set; }
        public string? InsurancePolicyDoc2FileName { get; set; }
        public string? InsurancePolicyDoc3FileName { get; set; }
        public string? InsurancePolicyDoc4FileName { get; set; }
        public string? InsurancePolicyDoc5FileName { get; set; }
        public string? OtherDocLinkFileName { get; set; }

        [DisplayName("Employer Name")]
        [Required]
        public string EmployerName { get; set; }

        [DisplayName("Group Policy Number")]
        [Required]
        public string GroupPolicyNumber { get; set; }
        [DisplayName("Employer Email Address")]
        [Required]
        public string EmployerEmailAddress { get; set; }

        [DisplayName("Employer Address")]
        [Required]
        public string EmployerAddress { get; set; }
        [DisplayName("Email Template Create")]
        [Required]
        public string EmailTemplateCreate { get; set; }
        [DisplayName("Email Template Update")]
        [Required]
        public string EmailTemplateUpdate { get; set; }
        [DisplayName("Email Template Insurance Plan Changed")]
        [Required]
        public string EmailTemplateInsurancePlanChanged { get; set; }
        [DisplayName("Email Template Family Information Updated")]
        
        [Required]
        public string EmailTemplateFamilyUpdated { get; set; }
        [DisplayName("HR Approve Template")]
        [Required]
        public string? HrApproveTemplate { get; set; }

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
            builder.Property(x => x.InsuranceCompanyName).HasColumnType("varchar(250)");

            builder.Property(x => x.InsurancePolicyDoc1).HasColumnType("varchar(Max)");
            builder.Property(x => x.InsurancePolicyDoc2).HasColumnType("varchar(Max)");
            builder.Property(x => x.InsurancePolicyDoc3).HasColumnType("varchar(Max)");
            builder.Property(x => x.InsurancePolicyDoc4).HasColumnType("varchar(Max)");
            builder.Property(x => x.InsurancePolicyDoc5).HasColumnType("varchar(Max)");
            builder.Property(x => x.OtherDocLink).HasColumnType("varchar(Max)");


            builder.Property(x => x.InsurancePolicyDoc1FileName).HasColumnType("varchar(250)");
            builder.Property(x => x.InsurancePolicyDoc2FileName).HasColumnType("varchar(250)");
            builder.Property(x => x.InsurancePolicyDoc3FileName).HasColumnType("varchar(250)");
            builder.Property(x => x.InsurancePolicyDoc4FileName).HasColumnType("varchar(250)");
            builder.Property(x => x.InsurancePolicyDoc5FileName).HasColumnType("varchar(250)");
            builder.Property(x => x.OtherDocLinkFileName).HasColumnType("varchar(250)");



            builder.Property(x => x.GroupPolicyNumber).HasColumnType("varchar(100)");
            builder.Property(x => x.EmailTemplateCreate).HasColumnType("varchar(500)");
            builder.Property(x => x.EmailTemplateUpdate).HasColumnType("varchar(500)");
            builder.Property(x => x.EmailTemplateInsurancePlanChanged).HasColumnType("varchar(500)");
            builder.Property(x => x.EmailTemplateFamilyUpdated).HasColumnType("varchar(500)");
            builder.Property(x => x.HrApproveTemplate).HasColumnType("varchar(500)");

            builder.Property(x => x.CreatedBy).HasColumnType("varchar(150)");
            builder.Property(x => x.UpdatedBy).HasColumnType("varchar(150)");

        }
    }
}
