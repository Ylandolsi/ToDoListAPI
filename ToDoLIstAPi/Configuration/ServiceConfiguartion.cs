using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ToDoLIstAPi.DbContext;

namespace ToDoLIstAPi.Configuration;

public static class ServiceConfiguartion
{

    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });




    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "toDoList api ", Version = "v1"
            });
        });
    }

    public static void ConfigureALl(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.ConfigureCors();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.ConfigureSwagger();
    }
}
