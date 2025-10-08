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

    public Tarefa Create(Tarefa tarefa)
    {
        _repository.Add(tarefa);
        _repository.SaveChanges();
        return tarefa;
    }

    public bool Delete(int id)
    {

        var tarefaDb = _repository.GetById(id);
        if (tarefaDb == null) return false;

        _repository.Delete(id);
        _repository.SaveChanges();
        return true;
    }

    public IEnumerable<Tarefa> GetAll() => _repository.GetAll();

    public List<Tarefa> GetByData(DateTime data) => _repository.GetByData(data);
    public Tarefa GetById(int id) => _repository.GetById(id);

    public List<Tarefa> GetByStatus(string status) => _repository.GetByStatus(status);

    public List<Tarefa> GetByTitulo(string titulo) => _repository.GetByTitulo(titulo);

    public Tarefa Update(int id, Tarefa tarefa)
    {
        var existente = _repository.GetById(id);
        if (existente == null) return null;

        existente.Titulo = tarefa.Titulo;
        existente.Descricao = tarefa.Descricao;
        existente.Data = DateTime.Now;
        existente.Status = tarefa.Status;
        _repository.Update(existente);
        _repository.SaveChanges();

        return existente;

    }
}
