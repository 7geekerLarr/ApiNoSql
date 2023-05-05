using ApiNoSqlApi.Middleware;
using ApiNoSqlApplication.Client.Queries;
using ApiNoSqlInfraestructure.Data;
using ApiNoSqlInfraestructure.Repository;
using ApiNoSqlInfraestructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true);

// Add services to the container.
builder.Services.AddMediatR(typeof(GetAllClientsQuery).Assembly, typeof(Program).Assembly);
builder.Services.AddDbContext<ClientsContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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




builder.Services.AddCors(o => o.AddPolicy("corsApp", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseCors("corsApp");
app.UseMiddleware<HandleErrorMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
