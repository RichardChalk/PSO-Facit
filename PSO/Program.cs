
using Microsoft.EntityFrameworkCore;
using SuperHeroAPIDemo_G_NET9.Data;

namespace SuperHeroAPIDemo_G_NET9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Lägg till min DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Lägg till min DataInitializer med Dependency Injection
            builder.Services.AddTransient<DataInitializer>();

            var app = builder.Build();

            // Kör min MigrateData() metod
            using (var scope = app.Services.CreateScope())
            {
                scope.ServiceProvider.GetService<DataInitializer>().MigrateData();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                // Re-introduce Swagger UI
                app.UseSwaggerUI(options =>
                    options.SwaggerEndpoint("/openapi/v1.json", "superHero api"));

            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
