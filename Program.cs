using Microsoft.EntityFrameworkCore;
using tarefas_api.Context;
using tarefas_api.Repositories;
using tarefas_api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetSection("DB_CONNECTION").Get<string>();
if(string.IsNullOrEmpty(connectionString))
    throw new Exception("ConnectionString não configurada");

var webapp = builder.Configuration.GetSection("MEU_APP").Get<string>();
if (string.IsNullOrEmpty(webapp))
    throw new Exception("WebApp não configurado!");

// Add services to the container.
builder.Services.AddDbContext<TarefaContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirMeuSite", policy =>
    {
        policy.WithOrigins(webapp)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader();
    });
});



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirMeuSite");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
