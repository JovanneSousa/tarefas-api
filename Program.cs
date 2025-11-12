using Microsoft.EntityFrameworkCore;
using tarefas_api.Context;
using tarefas_api.Repositories;
using tarefas_api.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
//var webapp = Environment.GetEnvironmentVariable("MEU_APP");

// Add services to the container.
builder.Services.AddDbContext<TarefaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString(connectionString)));
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirMeuSite", policy =>
    {
        policy.AllowAnyOrigin()
                .AllowCredentials()
                .AllowAnyMethod()
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
