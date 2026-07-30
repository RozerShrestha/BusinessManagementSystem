using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TattooAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class addedadvancepaymentsettlement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdvancePaymentSettlement",
                table: "AdvancePayments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvancePaymentSettlement",
                table: "AdvancePayments");
        }
    }
}

