using ToDoLIstAPi;
using ToDoLIstAPi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceConfiguartion.ConfigureALl(builder);

var app = builder.Build();
app.ConfigurePipeline(); 
app.Run();


