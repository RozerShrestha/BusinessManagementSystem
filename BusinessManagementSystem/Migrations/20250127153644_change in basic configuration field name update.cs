using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class changeinbasicconfigurationfieldnameupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvancePaymentSuperadmin",
                table: "BasicConfigurations",
                newName: "AdvancePaymentSuperadminTemplate");

            migrationBuilder.RenameColumn(
                name: "AdvancePaymentArtist",
                table: "BasicConfigurations",
                newName: "AdvancePaymentArtistTemplate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdvancePaymentSuperadminTemplate",
                table: "BasicConfigurations",
                newName: "AdvancePaymentSuperadmin");

            migrationBuilder.RenameColumn(
                name: "AdvancePaymentArtistTemplate",
                table: "BasicConfigurations",
                newName: "AdvancePaymentArtist");
        }
    }
}
