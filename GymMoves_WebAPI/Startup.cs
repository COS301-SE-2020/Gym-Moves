using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymMoves_WebAPI.Data.DBContext;
using GymMoves_WebAPI.Data.Repositories;
using GymMoves_WebAPI.Data.Repositories.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GymMoves_WebAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddScoped<UserRepositoryInterface, UserRepository>();
            services.AddScoped<ClassRepositoryInterface, ClassRepository>();
            services.AddScoped<GymRepositoryInterface, GymRepository>();
            services.AddDbContext<MainDatabaseContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("GymDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
