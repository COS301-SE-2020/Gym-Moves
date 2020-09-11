using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class ChangedGymAttendenceFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "TimeInterval",
                table: "GymAttendence");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "GymAttendence",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "GymAttendence",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "GymAttendence",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "GymAttendence",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence",
                columns: new[] { "GymId", "Time", "Day", "Month", "Year" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "GymAttendence");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "GymAttendence",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "GymAttendence",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TimeInterval",
                table: "GymAttendence",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence",
                columns: new[] { "GymId", "Date", "TimeInterval" });
        }
    }
}
