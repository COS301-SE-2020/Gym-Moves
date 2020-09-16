using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class AddDefaultSupportUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SupportStaff",
                columns: new[] { "Username", "Email", "Name", "Password", "Surname" },
                values: new object[] { "mastersupport", "kanglongjidev@gmail.com", "Master", "bd5779f74e521b9a7fab223b4a5d3eb9880b9e50a3e3b3e50c24b25b7ed896ef", "Support" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SupportStaff",
                keyColumn: "Username",
                keyValue: "mastersupport");
        }
    }
}
