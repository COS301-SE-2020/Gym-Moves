using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class AddGymAttendanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GymAttendence",
                columns: table => new
                {
                    GymId = table.Column<int>(nullable: false),
                    Time = table.Column<string>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymAttendence", x => new { x.GymId, x.Time, x.Day, x.Month, x.Year });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymAttendence");
        }
    }
}
