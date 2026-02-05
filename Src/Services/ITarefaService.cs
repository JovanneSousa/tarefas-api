using tarefas_api.Models;

namespace tarefas_api.Services;

public interface ITarefaService
{
    Task<IEnumerable<Tarefa>> GetAllAsync();
    Task<Tarefa> GetByIdAsync(int id);
    Task<List<Tarefa>> GetByTituloAsync(string titulo);
    Task<List<Tarefa>> GetByStatusAsync(string status);
    Task<List<Tarefa>> GetByDataAsync(DateTime data);
    Task<Tarefa> CreateAsync(Tarefa tarefa);
    Task<Tarefa> UpdateAsync(int id, Tarefa tarefa);
    Task<Tarefa> UpdateStatusAsync(int id, Tarefa tarefa);
    Task<bool> DeleteAsync(int id);
}
