using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TattooAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class OtpCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OtpCode",
                table: "OTPs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpCode",
                table: "OTPs");
        }
    }
}

