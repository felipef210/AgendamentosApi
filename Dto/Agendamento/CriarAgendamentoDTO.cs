using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Dto.Agendamento;

public class CriarAgendamentoDTO
{
    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required int Servico { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required DateTime DataHoraAgendamento { get; set; }
}
