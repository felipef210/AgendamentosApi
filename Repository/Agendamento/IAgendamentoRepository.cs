using AgendamentosApi.Dto.Agendamento;
using Microsoft.AspNetCore.Mvc;
using AgendamentoModel = AgendamentosApi.Models.Agendamento;

namespace AgendamentosApi.Repository.Agendamento;

public interface IAgendamentoRepository
{
    Task<List<AgendamentoModel>> ListarAgendamentos();
    Task<AgendamentoModel> BuscarAgendamentoPorId(int id);
    Task CriarAgendamento(AgendamentoModel agendamento);
    Task EditarAgendamento(AgendamentoModel agendamento, int id);
    Task ExcluirAgendamento(int id);
    Task<List<AgendamentoModel>> ListarAgendamentosPorUsuario(string id);
    Task<bool> DataHoraJaReservada(DateTime dataHora, int servicoId, int? ignorarId);
}
