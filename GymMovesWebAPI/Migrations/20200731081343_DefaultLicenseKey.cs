using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class DefaultLicenseKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gyms_GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Users_InstructorUsername",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_InstructorUsername",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorUsername",
                table: "Classes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Classes",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "LicenseKey", "Email" },
                values: new object[] { "testkey", "testmail@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymId",
                table: "Classes",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gyms_GymId",
                table: "Classes",
                column: "GymId",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gyms_GymId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_GymId",
                table: "Classes");

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "LicenseKey",
                keyValue: "testkey");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Classes");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorUsername",
                table: "Classes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GymIdGymIdForeignKey",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymIdGymIdForeignKey",
                table: "Classes",
                column: "GymIdGymIdForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorUsername",
                table: "Classes",
                column: "InstructorUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gyms_GymIdGymIdForeignKey",
                table: "Classes",
                column: "GymIdGymIdForeignKey",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Users_InstructorUsername",
                table: "Classes",
                column: "InstructorUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
