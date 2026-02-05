using Microsoft.EntityFrameworkCore;
using tarefas_api.Models;

namespace tarefas_api.Context;

public class TarefaContext : DbContext
{
    public TarefaContext(DbContextOptions<TarefaContext> options) : base(options) { }
    public DbSet<Tarefa> Tarefas { get; set; }
}