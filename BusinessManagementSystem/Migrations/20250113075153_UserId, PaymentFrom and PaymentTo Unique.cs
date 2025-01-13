using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UserIdPaymentFromandPaymentToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
            name: "IX_PaymentHistories_UserId_PaymentFrom_PaymentTo",
            table: "PaymentHistories",
            columns: new[] { "UserId", "PaymentFrom", "PaymentTo" },
            unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_PaymentHistories_UserId_PaymentFrom_PaymentTo",
            table: "PaymentHistories");
        }

    }
}
