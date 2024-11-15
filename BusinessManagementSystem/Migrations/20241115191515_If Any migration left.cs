using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class IfAnymigrationleft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tip_Appointments_AppointmentId",
                table: "Tip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tip",
                table: "Tip");

            migrationBuilder.RenameTable(
                name: "Tip",
                newName: "Tips");

            migrationBuilder.RenameIndex(
                name: "IX_Tip_AppointmentId",
                table: "Tips",
                newName: "IX_Tips_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tips",
                table: "Tips",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tips_Id",
                table: "Tips",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tips_Appointments_AppointmentId",
                table: "Tips",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tips_Appointments_AppointmentId",
                table: "Tips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tips",
                table: "Tips");

            migrationBuilder.DropIndex(
                name: "IX_Tips_Id",
                table: "Tips");

            migrationBuilder.RenameTable(
                name: "Tips",
                newName: "Tip");

            migrationBuilder.RenameIndex(
                name: "IX_Tips_AppointmentId",
                table: "Tip",
                newName: "IX_Tip_AppointmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tip",
                table: "Tip",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tip_Appointments_AppointmentId",
                table: "Tip",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
