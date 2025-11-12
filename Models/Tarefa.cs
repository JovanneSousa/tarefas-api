using System.ComponentModel.DataAnnotations;
using tarefas_api.Enums;

namespace tarefas_api.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; }

    [Required]
    public string Descricao { get; set; }

    [DataType(DataType.Date)]
    public DateTime Data { get; set; }

    [Required]
    public StatusTarefa Status { get; set; }

    [Required]
    public PrioridadeTarefa Prioridade { get; set; }
}
