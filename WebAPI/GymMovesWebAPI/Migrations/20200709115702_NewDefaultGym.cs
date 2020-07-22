using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class NewDefaultGym : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "GymId", "GymBranch", "GymName" },
                values: new object[] { 2, "TreeBranch", "AnotherGym" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: 2);
        }
    }
}
