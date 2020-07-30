using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class DefaultLicenseKeyFixClassForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gyms_GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "GymIdGymIdForeignKey",
                table: "Classes");

            migrationBuilder.InsertData(
                table: "Licenses",
                columns: new[] { "LicenseKey", "Email" },
                values: new object[] { "testkey", "testmail@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymIdForeignKey",
                table: "Classes",
                column: "GymIdForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gyms_GymIdForeignKey",
                table: "Classes",
                column: "GymIdForeignKey",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gyms_GymIdForeignKey",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_GymIdForeignKey",
                table: "Classes");

            migrationBuilder.DeleteData(
                table: "Licenses",
                keyColumn: "LicenseKey",
                keyValue: "testkey");

            migrationBuilder.AddColumn<int>(
                name: "GymIdGymIdForeignKey",
                table: "Classes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymIdGymIdForeignKey",
                table: "Classes",
                column: "GymIdGymIdForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Gyms_GymIdGymIdForeignKey",
                table: "Classes",
                column: "GymIdGymIdForeignKey",
                principalTable: "Gyms",
                principalColumn: "GymId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
