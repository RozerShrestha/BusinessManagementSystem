using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Basicconfigurationnefieldsadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdvancePaymentArtist",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdvancePaymentSuperadmin",
                table: "BasicConfigurations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancePaymentArtist",
                table: "BasicConfigurations");

            migrationBuilder.DropColumn(
                name: "AdvancePaymentSuperadmin",
                table: "BasicConfigurations");
        }
    }
}
