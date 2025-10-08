using tarefas_api.Models;

namespace tarefas_api.Repositories;

public interface ITarefaRepository
{
    IEnumerable<Models.Tarefa> GetAll();
    Tarefa GetById(int id);
    List<Tarefa> GetByTitulo(string titulo);
    List<Tarefa> GetByStatus(string status);
    List<Tarefa> GetByData(DateTime data);
    void Add(Tarefa tarefa);
    void Update(Tarefa tarefa);
    void Delete(int id);
    void SaveChanges();
}
