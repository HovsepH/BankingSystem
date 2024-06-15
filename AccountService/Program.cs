using AccountService.Application.Behaviors;
using AccountService.Application.Exceptions;
using AccountService.Application.Interfaces;
using AccountService.Infrastructure.Data;
using AccountService.Infrastructure.Repositories;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();

// Add FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add MediatR with behaviors
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Add custom exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add database connections and AutoMapper
builder.Services.AddConnections();
builder.Services.AddAutoMapper(typeof(Program));

// Configure DI for application services
builder.Services.AddScoped<ConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService.Infrastructure.Services.AccountService>();

// Configure database settings
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
