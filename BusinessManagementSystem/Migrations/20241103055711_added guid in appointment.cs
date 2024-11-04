using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addedguidinappointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hours",
                table: "Appointments",
                newName: "EstimatedHours");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "Appointments",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ArtistPreferance",
                table: "Appointments",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ConsentFormSigned",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FollowUpRequired",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InkColorPreferance",
                table: "Appointments",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicalConditions",
                table: "Appointments",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PainToleranceLevel",
                table: "Appointments",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Placement",
                table: "Appointments",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SessionNumber",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TattooDesign",
                table: "Appointments",
                type: "varchar(500)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "guid",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ArtistPreferance",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ConsentFormSigned",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "FollowUpRequired",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "InkColorPreferance",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "MedicalConditions",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PainToleranceLevel",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Placement",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SessionNumber",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TattooDesign",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "guid",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "EstimatedHours",
                table: "Appointments",
                newName: "Hours");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }
    }
}
