using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models.Exceptions;
using ToDoLIstAPi.DbContext;
using ToDoLIstAPi.ExceptionHandler;
using ToDoLIstAPi.Services;

namespace ToDoLIstAPi.Configuration;

public static class ServiceConfiguartion
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
        });

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "toDoList api ", Version = "v1" });
        });
    }

    public static void ConfigureUserService(this IServiceCollection services) =>
        services.AddScoped<ITaskService, TaskService>();

    public static void ConfigureALl(this WebApplicationBuilder builder)
    {

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // (problem : circular references when serializing the data ) 
                // Task have User and user Have task so we need to ignore the cycle
                // to avoid infinite loop ! 
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddOpenApi();
        builder.Services.ConfigureCors();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureUserService();
        
        builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<DbUpdateExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        builder.Services.AddProblemDetails(); 



    }
}
