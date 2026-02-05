using Microsoft.EntityFrameworkCore;
using tarefas_api.Context;
using tarefas_api.Enums;
using tarefas_api.Models;

namespace tarefas_api.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefaContext _context;

    public TarefaRepository(TarefaContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAsync(Tarefa tarefa)
    {
        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Tarefa tarefa) 
    {
        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Tarefa>> GetAllAsync() =>
        await _context.Tarefas.ToListAsync(); 

    public async Task<List<Tarefa>> GetByDataAsync(DateTime data)
        => await _context.Tarefas
              .Where(t => t.Data.Date == data.Date)
              .ToListAsync();

    public async Task<Tarefa?> GetByIdAsync(int id) 
        => await _context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<List<Tarefa>> GetByStatusAsync(string status)
    {
        if (!Enum.TryParse<StatusTarefa>(status, true, out var statusEnum))
            return new List<Tarefa>();
        return await _context.Tarefas
            .Where(t => t.Status == statusEnum)
            .ToListAsync();
    }

    public async Task<List<Tarefa>> GetByTituloAsync(string titulo)
        => await _context.Tarefas
                    .Where(t => t.Titulo.ToLower() == titulo.ToLower())
                    .ToListAsync();

    public async Task<bool> UpdateAsync(Tarefa tarefa) 
    {
        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }
}
