using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Repositories;
using UniversityAPI.Repositories.DormRepos;
using UniversityAPI.Repositories.UniBuildingRepos;

namespace UniversityAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<UniDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UniversityAPIConnectionString")));

            builder.Services.AddScoped<IUniversityRepository, SqlUniversityRepository>();
            builder.Services.AddScoped<IDormRepository, SqlDormRepository>();
            builder.Services.AddScoped<IUniversityBuildingRepository, SqlUniversityBuildingRepository>();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<UniversityAPI.Mappings.AutoMapperProfiles>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
