using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Register the custom aggregator
builder.Services.AddOcelot();
             

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Uncomment these lines if you have Swagger set up for development.
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseOcelot().Wait(); // Ensure Ocelot middleware is awaited

app.MapControllers();

app.Run();
