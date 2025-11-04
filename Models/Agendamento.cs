using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace AgendamentosApi.Models;

public class Agendamento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required int ServicoId { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [ForeignKey("ServicoId")]
    public required Servico Servico { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required DateTime DataHoraAgendamento { get; set; }

    [Required]
    public required string ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    public required Usuario Cliente { get; set; }
}