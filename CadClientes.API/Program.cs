using CadClientes.Domain.Abstractions;
using CadClientes.Infrastructure.Context;
using CadClientes.Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() // Adicione as origens permitidas aqui
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Configuração do SQL Server para Dapper
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("SQLServer")));

// Configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => 
    new MongoClient(builder.Configuration.GetConnectionString("MongoDB")));
builder.Services.AddSingleton<AppDbContextMongo>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return new AppDbContextMongo(client, "ClientesBranef");
});

// Registrar a connection string como um serviço
builder.Services.AddSingleton(sp => builder.Configuration.GetConnectionString("SQLServer"));

// Registro dos repositórios e Unit of Work
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPorteEmpresaRepository, PorteEmpresaRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var myhandlers = AppDomain.CurrentDomain.Load("CadClientes.Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myhandlers));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAngularOrigins");
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
