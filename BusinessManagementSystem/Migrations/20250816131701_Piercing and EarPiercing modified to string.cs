using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TattooAppointmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class PiercingandEarPiercingmodifiedtostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PiercingPrice",
                table: "BasicConfigurations",
                type: "varchar(1000)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "EarPiercingPrice",
                table: "BasicConfigurations",
                type: "varchar(1000)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EarPiercingPrice",
                table: "BasicConfigurations");

            migrationBuilder.AlterColumn<double>(
                name: "PiercingPrice",
                table: "BasicConfigurations",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1000)");
        }
    }
}

