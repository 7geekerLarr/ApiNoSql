using ApiNoSqlApi.Middleware;
using ApiNoSqlApplication.Client.Queries;
using ApiNoSqlInfraestructure.AutoMapper;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Repository;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Add services to the container.
builder.Services.AddMediatR(typeof(GetAllClientsQuery).Assembly, typeof(Program).Assembly);
builder.Services.AddDbContext<ClientsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Clients ", Version = "v1" });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure ClientsRepositoryConfiguration
var clientsRepoConfig = builder.Configuration.GetSection("ClientsRepositoryConfiguration")?.Get<ClientsRepositoryConfiguration>();

if (clientsRepoConfig != null)
{
    if (clientsRepoConfig.Type.ToString() == "SqlServer")
    {
        builder.Services.AddScoped<IClients, ClientsSqlRepository>();
    }
    else if (clientsRepoConfig.Type.ToString() == "MongoDB")
    {
        builder.Services.AddScoped<IClients, ClientsMongoRepository>();
    }
    else
    {
        throw new Exception($"Invalid repository type: {clientsRepoConfig.Type}");
    }
}
else
{
    throw new Exception("ClientsRepositoryConfiguration section not found in configuration file.");
}

builder.Services.AddCors(o => o.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ClientsContext>();

    // Generar la tabla "Client" si no existe
    context.Database.EnsureCreated();

    Console.WriteLine("Tabla 'Clients' Se crea si no existe!");
}

app.UseCors("corsApp");
app.UseMiddleware<HandleErrorMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Clients v1");
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
