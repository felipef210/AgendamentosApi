namespace AgendamentosApi.Dto.Agendamento;

public class AgendamentoDTO
{
    public int Id { get; set; }
    public string? Servico { get; set; }
    public DateTime DataHoraAgendamento { get; set; }
    public string? ClienteNome { get; set; }
    public string? ClienteEmail { get; set; }
    public string? ClienteTelefone { get; set; }
}
