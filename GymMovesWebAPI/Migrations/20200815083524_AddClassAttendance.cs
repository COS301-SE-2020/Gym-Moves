using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class AddClassAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassAttendance",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    NumberOfStudents = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassAttendance", x => new { x.ClassId, x.Date });
                    table.ForeignKey(
                        name: "FK_ClassAttendance_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassAttendance");
        }
    }
}
