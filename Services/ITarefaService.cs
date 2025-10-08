using tarefas_api.Models;

namespace tarefas_api.Services;

public interface ITarefaService
{
    IEnumerable<Tarefa> GetAll();
    Tarefa GetById(int id);
    List<Tarefa> GetByTitulo(string titulo);
    List<Tarefa> GetByStatus(string status);
    List<Tarefa> GetByData(DateTime data);
    Tarefa Create(Tarefa tarefa);
    Tarefa Update(int id, Tarefa tarefa);
    bool Delete(int id);
}
