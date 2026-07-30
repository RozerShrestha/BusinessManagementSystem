using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TattooAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AdvancePaymentSettlementAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAdvancePayment",
                table: "PaymentHistories",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAdvancePayment",
                table: "PaymentHistories");
        }
    }
}

