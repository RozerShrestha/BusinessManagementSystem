using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class OutletAddedinAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Outlet",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Outlet",
                table: "Appointments");
        }
    }
}
