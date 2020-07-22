using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class AddedDefaultValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GymMembers",
                columns: new[] { "MembershipId", "GymId", "Email", "Name", "PhoneNumber", "Surname", "UserType" },
                values: new object[,]
                {
                    { "testmanagermembershipid", 1, "managertestemail@gmail.com", "Test", "0629058357", "Manager", 2 },
                    { "testinstructormembershipid", 1, "instructortestemail@gmail.com", "Test", "0629058357", "Instructor", 1 },
                    { "testmembermembershipid", 1, "membertestemail@gmail.com", "Test", "0629058357", "Member", 0 }
                });

            migrationBuilder.InsertData(
                table: "Gyms",
                columns: new[] { "GymId", "GymBranch", "GymName" },
                values: new object[] { 1, "TestBranch", "TestName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumns: new[] { "MembershipId", "GymId" },
                keyValues: new object[] { "testinstructormembershipid", 1 });

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumns: new[] { "MembershipId", "GymId" },
                keyValues: new object[] { "testmanagermembershipid", 1 });

            migrationBuilder.DeleteData(
                table: "GymMembers",
                keyColumns: new[] { "MembershipId", "GymId" },
                keyValues: new object[] { "testmembermembershipid", 1 });

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: 1);
        }
    }
}
