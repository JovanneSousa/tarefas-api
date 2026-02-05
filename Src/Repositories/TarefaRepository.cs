using tarefas_api.Context;
using tarefas_api.Enums;
using tarefas_api.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tarefas_api.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefaContext _context;

    public TarefaRepository(TarefaContext context)
    {
        _context = context;
    }

    public void Add(Tarefa tarefa) => _context.Tarefas.Add(tarefa);

    public void Delete(int id) => _context.Tarefas.Remove(GetById(id));

    public IEnumerable<Tarefa> GetAll() => _context.Tarefas.ToList();

    public List<Tarefa> GetByData(DateTime data)
    {
        return _context.Tarefas
          .Where(t => t.Data.Date == data.Date)
          .ToList();
    }

    public Tarefa? GetById(int id) => _context.Tarefas.Find(id);

    public List<Tarefa> GetByStatus(string status)
    {
        if (!Enum.TryParse<StatusTarefa>(status, true, out var statusEnum))
            return new List<Tarefa>();
        return _context.Tarefas
            .Where(t => t.Status == statusEnum)
            .ToList();
    }

    public List<Tarefa> GetByTitulo(string titulo)
    {
        return _context.Tarefas
                  .Where(t => t.Titulo.ToLower() == titulo.ToLower())
                  .ToList();
    }

    public void SaveChanges() => _context.SaveChanges();

    public void Update(Tarefa tarefa) => _context.Tarefas.Update(tarefa);
}
