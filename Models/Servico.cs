using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Models;

public class Servico
{
    [Key]
    public required int Id { get; set; }
    public required string Nome { get; set; }
    public required int DuracaoEmMinutos { get; set; }
}
