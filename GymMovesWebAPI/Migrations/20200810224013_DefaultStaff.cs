using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class DefaultStaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "GymApplications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "SupportStaff",
                columns: new[] { "Username", "Email", "Name", "Password", "Surname" },
                values: new object[] { "test", "testmail@gmail.com", "Support", "testpass", "Test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SupportStaff",
                keyColumn: "Username",
                keyValue: "test");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "GymApplications");
        }
    }
}
