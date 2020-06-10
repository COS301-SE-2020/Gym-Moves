using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMoves_WebAPI.Migrations
{
    public partial class InitialGymDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassTimes",
                columns: table => new
                {
                    ClassTimeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTimes", x => x.ClassTimeID);
                });

            migrationBuilder.CreateTable(
                name: "ClassTypes",
                columns: table => new
                {
                    ClassTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassTypes", x => x.ClassTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Gyms",
                columns: table => new
                {
                    GymID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GymName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gyms", x => x.GymID);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    InstructorID = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RegisteredGymFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.InstructorID);
                    table.ForeignKey(
                        name: "FK_Instructors_Gyms_RegisteredGymFK",
                        column: x => x.RegisteredGymFK,
                        principalTable: "Gyms",
                        principalColumn: "GymID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    RegisteredGymFK = table.Column<int>(nullable: false),
                    ClassEntityClassID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Gyms_RegisteredGymFK",
                        column: x => x.RegisteredGymFK,
                        principalTable: "Gyms",
                        principalColumn: "GymID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassTypeFK = table.Column<int>(nullable: false),
                    InstructorIDFK = table.Column<string>(nullable: true),
                    AtGymFK = table.Column<int>(nullable: false),
                    ClassTimeFK = table.Column<int>(nullable: false),
                    maxCapacity = table.Column<int>(nullable: false),
                    registeredCount = table.Column<int>(nullable: false),
                    UserEntityUserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Classes_Gyms_AtGymFK",
                        column: x => x.AtGymFK,
                        principalTable: "Gyms",
                        principalColumn: "GymID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_ClassTimes_ClassTimeFK",
                        column: x => x.ClassTimeFK,
                        principalTable: "ClassTimes",
                        principalColumn: "ClassTimeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_ClassTypes_ClassTypeFK",
                        column: x => x.ClassTypeFK,
                        principalTable: "ClassTypes",
                        principalColumn: "ClassTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Instructors_InstructorIDFK",
                        column: x => x.InstructorIDFK,
                        principalTable: "Instructors",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Classes_Users_UserEntityUserID",
                        column: x => x.UserEntityUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_AtGymFK",
                table: "Classes",
                column: "AtGymFK");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTimeFK",
                table: "Classes",
                column: "ClassTimeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ClassTypeFK",
                table: "Classes",
                column: "ClassTypeFK");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_InstructorIDFK",
                table: "Classes",
                column: "InstructorIDFK");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_UserEntityUserID",
                table: "Classes",
                column: "UserEntityUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_RegisteredGymFK",
                table: "Instructors",
                column: "RegisteredGymFK");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassEntityClassID",
                table: "Users",
                column: "ClassEntityClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegisteredGymFK",
                table: "Users",
                column: "RegisteredGymFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Classes_ClassEntityClassID",
                table: "Users",
                column: "ClassEntityClassID",
                principalTable: "Classes",
                principalColumn: "ClassID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Gyms_AtGymFK",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Gyms_RegisteredGymFK",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gyms_RegisteredGymFK",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ClassTimes_ClassTimeFK",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ClassTypes_ClassTypeFK",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Instructors_InstructorIDFK",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Users_UserEntityUserID",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Gyms");

            migrationBuilder.DropTable(
                name: "ClassTimes");

            migrationBuilder.DropTable(
                name: "ClassTypes");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
