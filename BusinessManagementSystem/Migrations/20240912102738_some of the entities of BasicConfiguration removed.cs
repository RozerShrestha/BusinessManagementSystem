using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class someoftheentitiesofBasicConfigurationremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailTemplateCreate",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "EmailTemplateFamilyUpdated",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "EmailTemplateInsurancePlanChanged",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "EmailTemplateUpdate",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "GroupPolicyNumber",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "HrApproveTemplate",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsuranceCompanyName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc1",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc1FileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc2",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc2FileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc3",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc3FileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc4",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc4FileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc5",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyDoc5FileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "OtherDocLink",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "OtherDocLinkFileName",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "ShowProductWalkThrough",
                table: "BasicConfigurations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailTemplateCreate",
                table: "BasicConfigurations",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailTemplateFamilyUpdated",
                table: "BasicConfigurations",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailTemplateInsurancePlanChanged",
                table: "BasicConfigurations",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmailTemplateUpdate",
                table: "BasicConfigurations",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GroupPolicyNumber",
                table: "BasicConfigurations",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HrApproveTemplate",
                table: "BasicConfigurations",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsuranceCompanyName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc1",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc1FileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc2",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc2FileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc3",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc3FileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc4",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc4FileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc5",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InsurancePolicyDoc5FileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDocLink",
                table: "BasicConfigurations",
                type: "varchar(Max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDocLinkFileName",
                table: "BasicConfigurations",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowProductWalkThrough",
                table: "BasicConfigurations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
