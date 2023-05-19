using ApiNoSqlApi.Middleware;
using ApiNoSqlApplication.Client.Queries;
using ApiNoSqlInfraestructure.AutoMapper;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Repository;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Add services to the container.
builder.Services.AddMediatR(typeof(GetAllClientsQuery).Assembly, typeof(Program).Assembly);
builder.Services.AddDbContext<ClientsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

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
    else if (clientsRepoConfig.Type.ToString() == "CosmosDB")
    {
        builder.Services.AddScoped<IClients, ClientsCosmosRepository>();
        builder.Services.AddSingleton<CosmosClient>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var endpointUri = config.GetSection("CosmosDB:EndpointUri").Value ?? throw new Exception("EndpointUri is not configured.");
            var primaryKey = config.GetSection("CosmosDB:PrimaryKey").Value ?? throw new Exception("PrimaryKey is not configured.");
            return new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = "API Clients" });
        });
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Clients", Version = "v1" });
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<ClientsContext>();

    // Generar la tabla "Client" si no existe
    context.Database.EnsureCreated();

    Console.WriteLine("Tablas 'Clients' se crea si no existe!");
}

app.UseMiddleware<HandleErrorMiddleware>();

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
if (clientsRepoConfig.Type.ToString() == "CosmosDB")
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();
        var databaseName = builder.Configuration.GetSection("CosmosDB:DatabaseName").Value ?? throw new Exception("DatabaseName is not configured.");
        var containerName = builder.Configuration.GetSection("CosmosDB:ContainerName").Value ?? throw new Exception("ContainerName is not configured.");

        var databaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
        var containerResponse = await databaseResponse.Database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey");
    }
}

app.Run();
