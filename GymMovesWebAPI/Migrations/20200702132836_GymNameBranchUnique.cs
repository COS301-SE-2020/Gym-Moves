using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class GymNameBranchUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GymName",
                table: "Gyms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GymBranch",
                table: "Gyms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_GymBranch",
                table: "Gyms",
                column: "GymBranch",
                unique: true,
                filter: "[GymBranch] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gyms_GymName",
                table: "Gyms",
                column: "GymName",
                unique: true,
                filter: "[GymName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Gyms_GymBranch",
                table: "Gyms");

            migrationBuilder.DropIndex(
                name: "IX_Gyms_GymName",
                table: "Gyms");

            migrationBuilder.AlterColumn<string>(
                name: "GymName",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GymBranch",
                table: "Gyms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
