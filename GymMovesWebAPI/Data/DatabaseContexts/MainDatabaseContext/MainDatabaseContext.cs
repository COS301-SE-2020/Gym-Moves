using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Models.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext {
    public class MainDatabaseContext : DbContext{
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

        private readonly IConfiguration config = null;

        public MainDatabaseContext(DbContextOptions options, IConfiguration config = null) : base(options) {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            /* Comment out when running unit tests */
            if (config != null) {
                optionsBuilder.UseSqlServer(config.GetConnectionString("GymDb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
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
                .HasKey(p => new {p.ClassIdForeignKey, p.StudentUsernameForeignKey});
        }
    }
}
