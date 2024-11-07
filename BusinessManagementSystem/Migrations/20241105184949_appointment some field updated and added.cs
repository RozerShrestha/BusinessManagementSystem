using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class appointmentsomefieldupdatedandadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "Appointments",
                newName: "TotalHours");

            migrationBuilder.RenameColumn(
                name: "EstimatedHours",
                table: "Appointments",
                newName: "TotalCost");

            migrationBuilder.AddColumn<double>(
                name: "DiscountInHour",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountInHour",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "TotalHours",
                table: "Appointments",
                newName: "Fee");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Appointments",
                newName: "EstimatedHours");
        }
    }
}
