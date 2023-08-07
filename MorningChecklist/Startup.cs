namespace MorningChecklist
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MorningChecklist.Application;
    using MorningChecklist.Application.Services;
    using MorningChecklist.Application.Services.Interfaces;
    using MorningChecklist.Infrastructure;
    using MorningChecklist.Infrastructure.Data;
    using MorningChecklist.Infrastructure.Repositories;
    using MorningChecklist.Infrastructure.Repositories.Interfaces;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configure DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMorningChecklistService, UserMorningChecklistService>();

            // Register the MediatR request handlers
            services.RegisterRequestHandlers();

            // Register AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(); // Add Swagger/OpenAPI

            // Configure CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Use CORS
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwagger(); // Enable Swagger UI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Morning Checklist API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();

                // Seed database with test data
                DatabaseSeeder.SeedData(scope.ServiceProvider);
            }
        }
    }

}
