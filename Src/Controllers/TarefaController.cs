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
    public async Task<IActionResult> GetAll() =>
        Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Tarefa tarefa) =>
        CreatedAtAction(
            nameof(GetById), new {id = tarefa.Id}, await _service.CreateAsync(tarefa)
            );

    [HttpPost("atualiza-status/{id:int}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusTarefa status)
    {
        if(!ModelState .IsValid) return BadRequest(status);

        var tarefa = await _service.GetByIdAsync(id);
        if (tarefa == null) return NotFound("Tarefa não encontrada");

        tarefa.Status = status == 0 ?
            StatusTarefa.Pendente :
            StatusTarefa.Concluida;

        return Ok(await _service.UpdateAsync(id, tarefa));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
    {
        if (!ModelState.IsValid || id != tarefa.Id) return BadRequest(tarefa);

        var tarefaExistente = await _service.GetByIdAsync(id);
        if (tarefaExistente == null)
            return NotFound(new { mensagem = "Tarefa não encontrada" });
        return Ok(await _service.UpdateAsync(id, tarefa));
    }

    [HttpPost("excluir/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid) return BadRequest();

        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("BuscarPorStatus")]
    public async Task<IActionResult> GetByStatus(string status) =>
        Ok(await _service.GetByStatusAsync(status));

    [HttpGet("BuscarPorData")]
    public async Task<IActionResult> GetByData(DateTime data) =>
        Ok(await _service.GetByDataAsync(data));

    [HttpGet("BuscarPorTitulo")]
    public async Task<IActionResult> GetByTitulo(string titulo) =>
        Ok(await _service.GetByTituloAsync(titulo));
}
