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
        Ok(await _service.CreateAsync(tarefa));

    [HttpPut("atualiza-status/{id:int}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusTarefa status) => 
        Ok(await _service.UpdateStatusAsync(id, status));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa) =>
        Ok(await _service.UpdateAsync(id, tarefa));

    [HttpDelete("excluir/{id}")]
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
