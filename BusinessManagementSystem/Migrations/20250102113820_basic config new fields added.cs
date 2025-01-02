using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class basicconfignewfieldsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppointmentUpdateTemplateArtist",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentUpdateTemplateClient",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewAppointmentTemplateArtist",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewAppointmentTemplateClient",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewUserEmailTemplate",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentSettlementTemplateArtist",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentUpdateTemplateArtist",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "AppointmentUpdateTemplateClient",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "NewAppointmentTemplateArtist",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "NewAppointmentTemplateClient",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "NewUserEmailTemplate",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "PaymentSettlementTemplateArtist",
                table: "BasicConfigurations");
        }
    }
}
