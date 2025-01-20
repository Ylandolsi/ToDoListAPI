using Microsoft.AspNetCore.Mvc;
using ToDoLIstAPi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceConfiguartion.ConfigureALl(builder);
builder.Services.AddAutoMapper(typeof(Program));
// to enable custoum response from action
// exp : return BadRequest("some message")
// cuz [apiController] return a default response ( 400 - badRequest ) 
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();
app.UseExceptionHandler(); 


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API ");
});


app.UseStaticFiles(); // Enable static files ( html , css , js , images .. )  to be served
app.UseRouting(); // maps incoming requests to route handlers

app.UseCors("CorsPolicy"); // allowing or blocking  requests from different origins ( cross-origin requests )

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
