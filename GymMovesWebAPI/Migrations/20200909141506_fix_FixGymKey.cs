using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class fix_FixGymKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GymBranch",
                table: "Gyms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GymName",
                table: "Gyms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GymBranch",
                table: "Gyms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
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
    }
}
