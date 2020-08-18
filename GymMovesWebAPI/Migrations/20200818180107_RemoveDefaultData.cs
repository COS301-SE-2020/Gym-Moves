using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class RemoveDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "Gyms",
                keyColumn: "GymId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SupportStaff",
                keyColumn: "Username",
                keyValue: "test");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { 1, "TestBranch", "TestName" },
                    { 2, "TreeBranch", "AnotherGym" }
                });

            migrationBuilder.InsertData(
                table: "SupportStaff",
                columns: new[] { "Username", "Email", "Name", "Password", "Surname" },
                values: new object[] { "test", "testmail@gmail.com", "Support", "testpass", "Test" });
        }
    }
}
