using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMovesWebAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GymMembers",
                columns: table => new
                {
                    MembershipId = table.Column<string>(nullable: false),
                    GymId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Surname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymMembers", x => new { x.MembershipId, x.GymId });
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    GymId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymName = table.Column<string>(nullable: true),
                    GymBranch = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.GymId);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    LicenseKey = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.LicenseKey);
                });

            migrationBuilder.CreateTable(
                name: "SupportStaff",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportStaff", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymIdForeignKey = table.Column<int>(nullable: false),
                    Heading = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Gyms_GymIdForeignKey",
                        column: x => x.GymIdForeignKey,
                        principalTable: "Gyms",
                        principalColumn: "GymId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    GymIdForeignKey = table.Column<int>(nullable: false),
                    MembershipId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Gyms_GymIdForeignKey",
                        column: x => x.GymIdForeignKey,
                        principalTable: "Gyms",
                        principalColumn: "GymId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymIdForeignKey = table.Column<int>(nullable: false),
                    GymIdGymIdForeignKey = table.Column<int>(nullable: true),
                    InstructorUsername = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    MaxCapactiy = table.Column<int>(nullable: false),
                    CurrentStudents = table.Column<int>(nullable: false),
                    Cancelled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                    table.ForeignKey(
                        name: "FK_Classes_Gyms_GymIdGymIdForeignKey",
                        column: x => x.GymIdGymIdForeignKey,
                        principalTable: "Gyms",
                        principalColumn: "GymId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Users_InstructorUsername",
                        column: x => x.InstructorUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstructorRatings",
                columns: table => new
                {
                    InstructorUsernameForeignKey = table.Column<string>(nullable: false),
                    RatingSum = table.Column<int>(nullable: false),
                    RatingCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructorRatings", x => x.InstructorUsernameForeignKey);
                    table.ForeignKey(
                        name: "FK_InstructorRatings_Users_InstructorUsernameForeignKey",
                        column: x => x.InstructorUsernameForeignKey,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    UsernameForeignKey = table.Column<string>(nullable: false),
                    Email = table.Column<bool>(nullable: false),
                    Sms = table.Column<bool>(nullable: false),
                    PushNotifications = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.UsernameForeignKey);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_Users_UsernameForeignKey",
                        column: x => x.UsernameForeignKey,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRatings",
                columns: table => new
                {
                    ClassIdForeignKey = table.Column<int>(nullable: false),
                    RatingSum = table.Column<int>(nullable: false),
                    RatingCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRatings", x => x.ClassIdForeignKey);
                    table.ForeignKey(
                        name: "FK_ClassRatings_Classes_ClassIdForeignKey",
                        column: x => x.ClassIdForeignKey,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassRegisters",
                columns: table => new
                {
                    StudentUsernameForeignKey = table.Column<string>(nullable: false),
                    ClassIdForeignKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassRegisters", x => new { x.ClassIdForeignKey, x.StudentUsernameForeignKey });
                    table.ForeignKey(
                        name: "FK_ClassRegisters_Classes_ClassIdForeignKey",
                        column: x => x.ClassIdForeignKey,
                        principalTable: "Classes",
                        principalColumn: "ClassId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassRegisters_Users_StudentUsernameForeignKey",
                        column: x => x.StudentUsernameForeignKey,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_GymIdGymIdForeignKey",
                table: "Classes",
                column: "GymIdGymIdForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorUsername",
                table: "Classes",
                column: "InstructorUsername");

            migrationBuilder.CreateIndex(
                name: "IX_ClassRegisters_StudentUsernameForeignKey",
                table: "ClassRegisters",
                column: "StudentUsernameForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_GymIdForeignKey",
                table: "Notifications",
                column: "GymIdForeignKey");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GymIdForeignKey",
                table: "Users",
                column: "GymIdForeignKey");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassRatings");

            migrationBuilder.DropTable(
                name: "ClassRegisters");

            migrationBuilder.DropTable(
                name: "GymMembers");

            migrationBuilder.DropTable(
                name: "InstructorRatings");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "SupportStaff");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Gyms");
        }
    }
}
