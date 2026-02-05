using tarefas_api.Models;
using tarefas_api.Repositories;

namespace tarefas_api.Services;

public class TarefaService : ITarefaService
{
    public ITarefaRepository _repository;

    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Tarefa> CreateAsync(Tarefa tarefa)
    {
        var success = await _repository.AddAsync(tarefa);
        if(!success)
        {
            Console.WriteLine("Erro ao adicionar tarefa");
            return null;
        }
        return tarefa;
    }

    public async Task<bool> DeleteAsync(int id)
    {

        var tarefaDb = await _repository.GetByIdAsync(id);
        if (tarefaDb == null)
        {
            Console.WriteLine("Tarefa não encontrada");
            return false;
        }
        var success = await _repository.DeleteAsync(tarefaDb);
        if(!success)
        {
            Console.WriteLine("Erro ao deletar tarefa");
            return false;
        }
        return true;
    }

    public async Task<IEnumerable<Tarefa>> GetAllAsync() => 
        await _repository.GetAllAsync();

    public async Task<List<Tarefa>> GetByDataAsync(DateTime data) => 
        await _repository.GetByDataAsync(data);
    public async Task<Tarefa> GetByIdAsync(int id) => 
        await _repository.GetByIdAsync(id);

    public async Task<List<Tarefa>> GetByStatusAsync(string status) => 
        await _repository.GetByStatusAsync(status);

    public async Task<List<Tarefa>> GetByTituloAsync(string titulo) => 
        await _repository.GetByTituloAsync(titulo);

    public async Task<Tarefa> UpdateAsync(int id, Tarefa tarefa)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente == null)
        {
            Console.WriteLine("Tarefa não existe");
            return null;
        }

        existente.Titulo = tarefa.Titulo;
        existente.Descricao = tarefa.Descricao;
        existente.Data = DateTime.UtcNow;
        existente.Status = tarefa.Status;

        var resultado = await _repository.UpdateAsync(existente);

        if(!resultado)
        {
            Console.WriteLine("Erro ao atualizar a tarefa");
            return null;
        }

        return existente;

    }
}
