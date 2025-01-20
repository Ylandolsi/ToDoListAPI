using ToDoLIstAPi.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ServiceConfiguartion.ConfigureALl(builder);

var app = builder.Build();

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
