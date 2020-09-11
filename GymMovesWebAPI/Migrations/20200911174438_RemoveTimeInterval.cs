using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class RemoveTimeInterval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence");

            migrationBuilder.DropColumn(
                name: "TimeInterval",
                table: "GymAttendence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence",
                columns: new[] { "GymId", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GymAttendence",
                table: "GymAttendence");

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
