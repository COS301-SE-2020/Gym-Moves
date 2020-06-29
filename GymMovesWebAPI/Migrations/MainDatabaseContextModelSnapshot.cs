﻿// <auto-generated />
using System;
using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GymMovesWebAPI.Migrations
{
    [DbContext(typeof(MainDatabaseContext))]
    partial class MainDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GymMovesWebAPI.Data.Models.DatabaseModels.NotificationSettings", b =>
                {
                    b.Property<string>("UsernameForeignKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Email")
                        .HasColumnType("bit");

                    b.Property<bool>("PushNotifications")
                        .HasColumnType("bit");

                    b.Property<bool>("Sms")
                        .HasColumnType("bit");

                    b.HasKey("UsernameForeignKey");

                    b.ToTable("NotificationSettings");
                });

            modelBuilder.Entity("GymMovesWebAPI.Data.Models.VerificationDatabaseModels.GymMember", b =>
                {
                    b.Property<string>("MembershipId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("GymId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("MembershipId", "GymId");

                    b.ToTable("GymMembers");

                    b.HasData(
                        new
                        {
                            MembershipId = "testmanagermembershipid",
                            GymId = 1,
                            Email = "managertestemail@gmail.com",
                            Name = "Test",
                            PhoneNumber = "0629058357",
                            Surname = "Manager",
                            UserType = 2
                        },
                        new
                        {
                            MembershipId = "testinstructormembershipid",
                            GymId = 1,
                            Email = "instructortestemail@gmail.com",
                            Name = "Test",
                            PhoneNumber = "0629058357",
                            Surname = "Instructor",
                            UserType = 1
                        },
                        new
                        {
                            MembershipId = "testmembermembershipid",
                            GymId = 1,
                            Email = "membertestemail@gmail.com",
                            Name = "Test",
                            PhoneNumber = "0629058357",
                            Surname = "Member",
                            UserType = 0
                        });
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.ClassRating", b =>
                {
                    b.Property<int>("ClassIdForeignKey")
                        .HasColumnType("int");

                    b.Property<int>("RatingCount")
                        .HasColumnType("int");

                    b.Property<int>("RatingSum")
                        .HasColumnType("int");

                    b.HasKey("ClassIdForeignKey");

                    b.ToTable("ClassRatings");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.ClassRegister", b =>
                {
                    b.Property<int>("ClassIdForeignKey")
                        .HasColumnType("int");

                    b.Property<string>("StudentUsernameForeignKey")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClassIdForeignKey", "StudentUsernameForeignKey");

                    b.HasIndex("StudentUsernameForeignKey");

                    b.ToTable("ClassRegisters");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.Gym", b =>
                {
                    b.Property<int>("GymId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GymBranch")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GymName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GymId");

                    b.ToTable("Gyms");

                    b.HasData(
                        new
                        {
                            GymId = 1,
                            GymBranch = "TestBranch",
                            GymName = "TestName"
                        });
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.GymClasses", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<int>("CurrentStudents")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GymIdForeignKey")
                        .HasColumnType("int");

                    b.Property<int?>("GymIdGymIdForeignKey")
                        .HasColumnType("int");

                    b.Property<string>("InstructorUsername")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClassId");

                    b.HasIndex("GymIdGymIdForeignKey");

                    b.HasIndex("InstructorUsername");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.InstructorRating", b =>
                {
                    b.Property<string>("InstructorUsernameForeignKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RatingCount")
                        .HasColumnType("int");

                    b.Property<int>("RatingSum")
                        .HasColumnType("int");

                    b.HasKey("InstructorUsernameForeignKey");

                    b.ToTable("InstructorRatings");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.LicenseKeys", b =>
                {
                    b.Property<string>("LicenseKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LicenseKey");

                    b.ToTable("Licenses");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.Notifications", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GymIdForeignKey")
                        .HasColumnType("int");

                    b.Property<string>("Heading")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NotificationId");

                    b.HasIndex("GymIdForeignKey");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.SupportUsers", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("SupportStaff");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.Users", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GymIdForeignKey")
                        .HasColumnType("int");

                    b.Property<string>("MembershipId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Username");

                    b.HasIndex("GymIdForeignKey");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GymMovesWebAPI.Data.Models.DatabaseModels.NotificationSettings", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Users", "User")
                        .WithOne("NotificationSetting")
                        .HasForeignKey("GymMovesWebAPI.Data.Models.DatabaseModels.NotificationSettings", "UsernameForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.ClassRating", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.GymClasses", "Class")
                        .WithOne("ClassRating")
                        .HasForeignKey("GymMovesWebAPI.Models.DatabaseModels.ClassRating", "ClassIdForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.ClassRegister", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.GymClasses", "Class")
                        .WithMany("Registers")
                        .HasForeignKey("ClassIdForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Users", "Student")
                        .WithMany("ClassRegisters")
                        .HasForeignKey("StudentUsernameForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.GymClasses", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Gym", "Gym")
                        .WithMany("Classes")
                        .HasForeignKey("GymIdGymIdForeignKey");

                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Users", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorUsername");
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.InstructorRating", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Users", "Instructor")
                        .WithOne("Rating")
                        .HasForeignKey("GymMovesWebAPI.Models.DatabaseModels.InstructorRating", "InstructorUsernameForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.Notifications", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Gym", "Gym")
                        .WithMany("Notifications")
                        .HasForeignKey("GymIdForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GymMovesWebAPI.Models.DatabaseModels.Users", b =>
                {
                    b.HasOne("GymMovesWebAPI.Models.DatabaseModels.Gym", "Gym")
                        .WithMany("Users")
                        .HasForeignKey("GymIdForeignKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
