using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi_csharp.Repository;
using WebApi_csharp.Services;

namespace WebApi_csharp
{
    public class StartupApp
    {
        public static void Run(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //inyecto servicio y repositorio 
            builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
            builder.Services.AddScoped<IProductoService,ProductoService>();
            
            // Add services to the container.
            builder.Services.AddControllers();

            

            // Agregar Swagger
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "LISI-UADER-SO",
                    Version = "v1",
                    Description = "API de productos - SO - FCyT - UADER"
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors();
                app.UseSwagger();              // genera /swagger/v1/swagger.json
                app.UseSwaggerUI(c =>          // UI en /swagger/index.html
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API V1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
