using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMoves_WebAPI.Migrations
{
    public partial class BaseInitialValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserType",
                table: "Users",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "ClassTimes",
                columns: new[] { "ClassTimeID", "Day", "Time" },
                values: new object[] { 1, "Wednesday", "16:00" });

            migrationBuilder.InsertData(
                table: "ClassTypes",
                columns: new[] { "ClassTypeID", "ClassName" },
                values: new object[] { 1, "TestClass" });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "GymID", "GymName" },
                values: new object[] { 1, "TestGym" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClassTimes",
                keyColumn: "ClassTimeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClassTypes",
                keyColumn: "ClassTypeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymID",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
