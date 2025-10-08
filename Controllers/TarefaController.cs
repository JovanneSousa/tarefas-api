using Microsoft.AspNetCore.Mvc;
using tarefas_api.Models;
using tarefas_api.Services;

namespace tarefas_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaService _service;

    public TarefaController(ITarefaService service)
    {
        _service = service;
    }

    [HttpGet("ObterTodos")]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var tarefa = _service.GetById(id);
        if (tarefa == null)
            return NotFound(new { mensagem = "Tarefa não encontrada" });
        return Ok(tarefa);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Tarefa tarefa)
    {
        var nova = _service.Create(tarefa);
        return CreatedAtAction(nameof(GetById), new { id = nova.Id }, nova);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Tarefa tarefa)
    {
        var atualizada = _service.Update(id, tarefa);
        if (atualizada == null)
            return NotFound(new { mensagem = "Tarefa não encontrada" });
        return Ok(atualizada);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var removida = _service.Delete(id);
        if (!removida)
            return NotFound(new { mensagem = "Tarefa não encontrada" });
        return NoContent();
    }

    [HttpGet("BuscarPorStatus")]
    public IActionResult GetByStatus(string status)
    {
        var tarefas = _service.GetByStatus(status);
        if (tarefas == null || !tarefas.Any())
            return NotFound(new { mensagem = "Nenhuma tarefa encontrada com o status fornecido" });
        return Ok(tarefas);
    }

    [HttpGet("BuscarPorData")]
    public IActionResult GetByData(DateTime data)
    {
        var tarefas = _service.GetByData(data);
        if (tarefas == null || !tarefas.Any())
            return NotFound(new { mensagem = "Nenhuma tarefa encontrada na data fornecida" });
        return Ok(tarefas);
    }

    [HttpGet("BuscarPorTitulo")]
    public IActionResult GetByTitulo(string titulo)
    {
        var tarefas = _service.GetByTitulo(titulo);
        if (tarefas == null || !tarefas.Any())
            return NotFound(new { mensagem = "Nenhuma tarefa encontrada com o título fornecido" });
        return Ok(tarefas);
    }
}
