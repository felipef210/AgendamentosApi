using AgendamentosApi.Data;
using AgendamentosApi.Dto.Agendamento;
using Microsoft.EntityFrameworkCore;
using AgendamentoModel = AgendamentosApi.Models.Agendamento;

namespace AgendamentosApi.Repository.Agendamento;

public class AgendamentoRepository : IAgendamentoRepository
{
    private readonly Context _context;

    public AgendamentoRepository(Context context)
    {
        _context = context;
    }

    public async Task<AgendamentoModel> BuscarAgendamentoPorId(int id)
    {
        var agendamento = await _context.Agendamentos
            .Include(a => a.Cliente)
            .FirstOrDefaultAsync(a => a.Id == id);

        return agendamento!;
    }

    public async Task CriarAgendamento(AgendamentoModel agendamento)
    {
        _context.Add(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task EditarAgendamento(AgendamentoModel agendamento, int id)
    {
        _context.Update(agendamento);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAgendamento(int id)
    {
        var agendamento = await _context.Agendamentos.FirstOrDefaultAsync(a => a.Id == id);
        _context.Remove(agendamento!);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AgendamentoModel>> ListarAgendamentos()
    {
        var agendamentos = await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(s => s.Servico)
            .ToListAsync();
        return agendamentos;
    }

    public async Task<List<AgendamentoModel>> ListarAgendamentosPorUsuario(string id)
    {
        var agendamentos = await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(s => s.Servico)
            .Where(a => a.ClienteId == id)
            .ToListAsync();

        return agendamentos;
    }

    public async Task<bool> DataHoraJaReservada(DateTime dataHora, int servicoId, int? ignorarId = null)
    {
        var servicoAtual = await _context.Servicos.FindAsync(servicoId);

        if (servicoAtual == null)
            throw new Exception("Serviço não encontrado.");


        var inicioServico = dataHora;
        var fimServico = dataHora.AddMinutes(servicoAtual.DuracaoEmMinutos);

        var query = _context.Agendamentos
            .Include(a => a.Servico)
            .AsQueryable();

        if (ignorarId.HasValue)
            query = query.Where(a => a.Id != ignorarId.Value);


        var agendamentoExiste = await query.AnyAsync(a =>
            (inicioServico < a.DataHoraAgendamento.AddMinutes(a.Servico.DuracaoEmMinutos)) && a.DataHoraAgendamento < fimServico);

        return agendamentoExiste;
    }
}
