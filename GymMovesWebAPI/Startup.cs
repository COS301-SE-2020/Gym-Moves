using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using GymMovesWebAPI.Data.Models.DatabaseModels;
using GymMovesWebAPI.Data.Repositories.Implementations;
using GymMovesWebAPI.Data.Repositories.Interfaces;
using GymMovesWebAPI.MailerProgram;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GymMovesWebAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddControllersWithViews();

            services.AddScoped<IClassRegisterRepository, ClassRegisterRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IGymRepository, GymRepository>();
            services.AddScoped<INotificationsRepository, NotificationsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INotificationSettingsRepository, NotificationSettingsRepository>();
            services.AddScoped<IGymMemberRepository, GymMemberRepository>();
            services.AddScoped<IPasswordResetRepository, PasswordResetRepository>();
            services.AddScoped<IClassRatingRepository, ClassRatingRepository>();
            services.AddScoped<IInstructorRatingRepository, InstructorRatingRepository>();
            services.AddScoped<IGymApplicationRepository, GymApplicationRepository>();
            services.AddScoped<ISupportStaffRepository, SupportStaffRepository>();
            services.AddScoped<IClassAttendanceRepository, ClassAttendanceRepository>();
            services.AddScoped<IApplicationCodeRepository, ApplicationCodeRepository>();

            services.AddScoped<IMailer, Mailer>();

            services.AddDbContext<MainDatabaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("GymDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }

}
        
