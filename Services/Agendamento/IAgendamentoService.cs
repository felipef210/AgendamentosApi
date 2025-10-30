using AgendamentosApi.Dto.Agendamento;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentosApi.Services.Agendamento;

public interface IAgendamentoService
{
    Task<List<AgendamentoDTO>> ListarAgendamentos();
    Task<AgendamentoDTO> BuscarAgendamentoPorId(int id);
    Task<AgendamentoDTO> CriarAgendamento([FromBody] CriarAgendamentoDTO criarAgendamentoDTO);
    Task<AgendamentoDTO> EditarAgendamento([FromBody] CriarAgendamentoDTO editarAgendamentoDTO, int id);
    Task ExcluirAgendamento(int id);
    Task<List<AgendamentoDTO>> ListarAgendamentosPorUsuario();
}
