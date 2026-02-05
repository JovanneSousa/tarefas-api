using Microsoft.AspNetCore.Mvc;
using tarefas_api.Enums;
using tarefas_api.Models;
using tarefas_api.Services;
using tarefas_api.Src.Controllers;

namespace tarefas_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : MainController
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
        if (!ModelState.IsValid) return BadRequest(tarefa);

        var nova = _service.Create(tarefa);
        return CreatedAtAction(nameof(GetById), new { id = nova.Id }, nova);
    }

    [HttpPost("atualiza-status/{id:int}")]
    public IActionResult UpdateStatus(int id, [FromBody] StatusTarefa status)
    {
        if(!ModelState .IsValid) return BadRequest(status);

        var tarefa = _service.GetById(id);
        if (tarefa == null) return NotFound("Tarefa não encontrada");

        tarefa.Status = status == 0 ?
            StatusTarefa.Pendente :
            StatusTarefa.Concluida;

        _service.Update(id, tarefa);

        return Ok(tarefa);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Tarefa tarefa)
    {
        if (!ModelState.IsValid || id != tarefa.Id) return BadRequest(tarefa);

        var tarefaExistente = _service.GetById(id);
        if (tarefaExistente == null)
            return NotFound(new { mensagem = "Tarefa não encontrada" });
        
        _service.Update(id, tarefa);
        return Ok(tarefa);
    }

    [HttpPost("excluir/{id}")]
    public IActionResult Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest();

        var tarefaExistente = _service.GetById(id);
        if (tarefaExistente == null)
            return NotFound(new { mensagem = "Tarefa não encontrada" });

        _service.Delete(id);
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
