using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class AddGymRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GymAttendence",
                columns: table => new
                {
                    GymId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TimeInterval = table.Column<string>(nullable: false),
                    Day = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymAttendence", x => new { x.GymId, x.Date, x.TimeInterval });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymAttendence");
        }
    }
}
