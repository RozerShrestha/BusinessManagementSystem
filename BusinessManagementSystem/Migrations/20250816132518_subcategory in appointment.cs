using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TattooAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class subcategoryinappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "Appointments");
        }
    }
}

