using GymMoves_WebAPI.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GymMoves_WebAPI.Data.DBContext {
    public class MainDatabaseContext : DbContext {
        public DbSet<GymEntity> Gyms { get; set; }
        public DbSet<ClassTypeEntity> ClassTypes { get; set; }
        public DbSet<InstructorEntity> Instructors { get; set; }
        public DbSet<ClassTimesEntity> ClassTimes { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ClassEntity> Classes { get; set; }

        private readonly IConfiguration _config;
        private DbContextOptions<MainDatabaseContext> options;

        public MainDatabaseContext(DbContextOptions options, IConfiguration config) : base(options) {
            _config = config;
        }

        public MainDatabaseContext(DbContextOptions<MainDatabaseContext> options) : base(options) {
   
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            /* Comment out when running unit tests */
            //optionsBuilder.UseSqlServer(_config.GetConnectionString("GymDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<GymEntity>()
                .HasMany(p => p.Users)
                .WithOne(p => p.RegisteredGym);

            modelBuilder.Entity<GymEntity>()
                .HasData(
                    new {
                        GymID = 1,
                        GymName = "TestGym"
                    }
                );

            modelBuilder.Entity<ClassTypeEntity>()
                .HasData(
                    new {
                        ClassTypeID = 1,
                        ClassName = "TestClass"
                    }
                );

            modelBuilder.Entity<ClassTimesEntity>()
                .HasData(
                    new {
                        ClassTimeID = 1,
                        Day = "Wednesday",
                        Time = "16:00"
                    }
                );

            modelBuilder.Entity<InstructorEntity>();

            modelBuilder.Entity<UserEntity>()
                .HasMany(p => p.Classes);

            modelBuilder.Entity<ClassEntity>()
                .HasMany(p => p.Students);
        }
    }
}
