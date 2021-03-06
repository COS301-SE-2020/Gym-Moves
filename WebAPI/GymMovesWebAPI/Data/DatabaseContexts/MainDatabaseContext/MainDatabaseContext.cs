﻿/*
File Name:
    MainDatabaseContext.cs

Author:
    Longji

Date Created:
    28/06/2020

Update History:
--------------------------------------------------------------------------------
Date          |    Author      |     Changes
--------------------------------------------------------------------------------
28/06/2020    |  Longji        |  Initial creation of the database context class.
--------------------------------------------------------------------------------
02/07/2020    |  Longji        |  Added table for password resets.
--------------------------------------------------------------------------------
09/07/2020    |  Longji        |  Added another default gym
--------------------------------------------------------------------------------


Functional Description:
    

List of Classes:
    - MainDatabaseContext
*/

using GymMovesWebAPI.Data.Enums;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Models.VerificationDatabaseModels;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext
{
    public class MainDatabaseContext : DbContext
    {
        public DbSet<ClassRating> ClassRatings { get; set; }
        public DbSet<ClassRegister> ClassRegisters { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<GymClasses> Classes { get; set; }
        public DbSet<InstructorRating> InstructorRatings { get; set; }
        public DbSet<LicenseKeys> Licenses { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }
        public DbSet<SupportUsers> SupportStaff { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<GymMember> GymMembers { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }

        private readonly IConfiguration config = null;

        public MainDatabaseContext(DbContextOptions options, IConfiguration config = null) : base(options)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* Comment out when running unit tests */
            if (config != null)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("GymDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GymMember>()
                .HasKey(p => new { p.MembershipId, p.GymId });

            modelBuilder.Entity<Gym>()
                .HasIndex(p => p.GymName)
                .IsUnique();

            modelBuilder.Entity<Gym>()
                .HasIndex(p => p.GymBranch)
                .IsUnique();

            modelBuilder.Entity<Gym>()
                .HasMany(p => p.Classes)
                .WithOne(p => p.Gym);

            modelBuilder.Entity<Gym>()
                .HasMany(p => p.Notifications)
                .WithOne(p => p.Gym);

            modelBuilder.Entity<Gym>()
                .HasMany(p => p.Users)
                .WithOne(p => p.Gym);

            modelBuilder.Entity<GymClasses>()
                .HasOne(p => p.Gym)
                .WithMany(p => p.Classes);

            modelBuilder.Entity<GymClasses>()
               .HasOne(p => p.Instructor);

            modelBuilder.Entity<GymClasses>()
               .HasOne(p => p.ClassRating)
               .WithOne(p => p.Class);

            modelBuilder.Entity<GymClasses>()
                .HasMany(p => p.Registers)
                .WithOne(p => p.Class);

            modelBuilder.Entity<ClassRating>()
                .HasOne(p => p.Class)
                .WithOne(p => p.ClassRating);

            modelBuilder.Entity<InstructorRating>()
                .HasOne(p => p.Instructor)
                .WithOne(p => p.Rating);

            modelBuilder.Entity<Notifications>()
                .HasOne(p => p.Gym)
                .WithMany(p => p.Notifications);

            modelBuilder.Entity<NotificationSettings>()
                .HasOne(p => p.User)
                .WithOne(p => p.NotificationSetting);

            modelBuilder.Entity<Users>()
                .HasOne(p => p.Gym)
                .WithMany(p => p.Users);

            modelBuilder.Entity<Users>()
                .HasOne(p => p.Rating)
                .WithOne(p => p.Instructor);

            modelBuilder.Entity<Users>()
                .HasOne(p => p.NotificationSetting)
                .WithOne(p => p.User);

            modelBuilder.Entity<Users>()
               .HasMany(p => p.ClassRegisters)
               .WithOne(p => p.Student);

            modelBuilder.Entity<ClassRegister>()
                .HasKey(p => new { p.ClassIdForeignKey, p.StudentUsernameForeignKey });

            modelBuilder.Entity<PasswordReset>()
                .HasIndex(p => p.Code)
                .IsUnique();

            /* Default data for gym */
            modelBuilder.Entity<Gym>()
                .HasData(
                    new
                    {
                        GymId = 1,
                        GymName = "TestName",
                        GymBranch = "TestBranch"
                    }
                );

            modelBuilder.Entity<Gym>()
                .HasData(
                    new {
                        GymId = 2,
                        GymName = "AnotherGym",
                        GymBranch = "TreeBranch"
                    }
                );

            /* Default data for verification table */
            modelBuilder.Entity<GymMember>()
                .HasData(
                    new
                    {
                        MembershipId = "testmanagermembershipid",
                        GymId = 1,
                        Name = "Test",
                        Surname = "Manager",
                        Email = "managertestemail@gmail.com",
                        PhoneNumber = "0629058357",
                        UserType = UserTypes.Manager
                    }
                );

            modelBuilder.Entity<GymMember>()
                .HasData(
                    new
                    {
                        MembershipId = "testinstructormembershipid",
                        GymId = 1,
                        Name = "Test",
                        Surname = "Instructor",
                        Email = "instructortestemail@gmail.com",
                        PhoneNumber = "0629058357",
                        UserType = UserTypes.Instructor
                    }
                );

            modelBuilder.Entity<GymMember>()
                .HasData(
                    new
                    {
                        MembershipId = "testmembermembershipid",
                        GymId = 1,
                        Name = "Test",
                        Surname = "Member",
                        Email = "membertestemail@gmail.com",
                        PhoneNumber = "0629058357",
                        UserType = UserTypes.Member
                    }
                );
        }
    }
}
