using tarefas_api.Models;

namespace tarefas_api.Repositories;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> GetAllAsync();
    Task<Tarefa> GetByIdAsync(int id);
    Task<List<Tarefa>> GetByTituloAsync(string titulo);
    Task<List<Tarefa>> GetByStatusAsync(string status);
    Task<List<Tarefa>> GetByDataAsync(DateTime data);
    Task<bool> AddAsync(Tarefa tarefa);
    Task<bool> UpdateAsync(Tarefa tarefa);
    Task<bool> DeleteAsync(Tarefa tarefa);
}
