using Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models.Exceptions;
using ToDoLIstAPi.Authentication;
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
 
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "toDoList api ", Version = "v1" });
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "basic",
                In = ParameterLocation.Header,
                Description = "Basic Authorization header."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    new string[] {}
                }
            });
        });

    }

    public static void ConfigureTaskService(this IServiceCollection services) =>
        services.AddScoped<ITaskService, TaskService>();
    
    public static void ConfigureUserService(this IServiceCollection services) =>
        services.AddScoped<IUserService, UserService>();
    
    public static void ConfigureAuthService (this IServiceCollection services) =>
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    

    public static void ConfigureALl(this WebApplicationBuilder builder)
    {

        
        
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // (problem : circular references when serializing the data ) 
                // Task have User and user Have task so we need to ignore the cycle
                // to avoid infinite loop !  ( replace the cycle with null and print the others ) 
                
                // or we can just use [JsonIgnore] on the navigation property ORRR Dto for repsponse
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        
        
        builder.Services.AddOpenApi();
        builder.Services.ConfigureCors();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureTaskService();
        builder.Services.ConfigureUserService();
        builder.Services.ConfigureAuthService();
        

        
        builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddExceptionHandler<DbUpdateExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddAuthentication("BasicAuthentication")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);

        // Enable authorization
        builder.Services.AddAuthorization();
        builder.Services.AddProblemDetails(); 



    }
}
