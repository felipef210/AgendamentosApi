using AgendamentosApi.Dto.Agendamento;
using AgendamentosApi.Repository.Agendamento;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuarioModel = AgendamentosApi.Models.Usuario;
using AgendamentoModel = AgendamentosApi.Models.Agendamento;

namespace AgendamentosApi.Services.Agendamento;

public class AgendamentoService : IAgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<UsuarioModel> _userManager;
    private readonly IMapper _mapper;

    public AgendamentoService(IAgendamentoRepository agendamentoRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, UserManager<UsuarioModel> userManager)
    {
        _agendamentoRepository = agendamentoRepository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<AgendamentoDTO> BuscarAgendamentoPorId(int id)
    {
        var agendamento = await _agendamentoRepository.BuscarAgendamentoPorId(id);

        if (agendamento is null)
            throw new NaoEncontradoException("Agendamento não encontrado");

        var agendamentoDTO = _mapper.Map<AgendamentoDTO>(agendamento);

        return agendamentoDTO;
    }

    public async Task<AgendamentoDTO> CriarAgendamento([FromBody] CriarAgendamentoDTO criarAgendamentoDTO)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");

        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        if (criarAgendamentoDTO.DataHoraAgendamento < DateTime.UtcNow)
            throw new ParametroInvalidoException("A data do agendamento deve ser a frente da data e hora atual.");

        if (await DataHoraJaReservada(criarAgendamentoDTO.DataHoraAgendamento))
            throw new ParametroInvalidoException("Horário de agendamento indisponível, por favor selecione outro horário.");

        var agendamento = _mapper.Map<AgendamentoModel>(criarAgendamentoDTO);
        agendamento.Cliente = usuario;
        agendamento.ClienteId = usuario.Id;

        await _agendamentoRepository.CriarAgendamento(agendamento);

        var agendamentoDTO = _mapper.Map<AgendamentoDTO>(agendamento);
        return agendamentoDTO;
    }

    public async Task<AgendamentoDTO> EditarAgendamento([FromBody] CriarAgendamentoDTO editarAgendamentoDTO, int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");

        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var agendamento = await _agendamentoRepository.BuscarAgendamentoPorId(id);

        if (agendamento is null)
            throw new Exception("Agendamento não encontrado");

        if (agendamento.ClienteId != usuario.Id && !usuario.IsAdmin)
            throw new ApplicationException("Você não pode editar agendamento de outro usuário");

        if (editarAgendamentoDTO.DataHoraAgendamento < DateTime.UtcNow)
            throw new ParametroInvalidoException("A data do agendamento deve ser a frente da data e hora atual.");
        
        if (await DataHoraJaReservada(editarAgendamentoDTO.DataHoraAgendamento, id))
            throw new ParametroInvalidoException("Horário de agendamento indisponível, por favor selecione outro horário.");
        
        _mapper.Map(editarAgendamentoDTO, agendamento);

        if (usuario.Id == agendamento.ClienteId)
        {
            agendamento.Cliente = usuario;
            agendamento.ClienteId = usuario.Id;
        }

        else
        {
            agendamento.Cliente = agendamento.Cliente;
            agendamento.ClienteId = agendamento.ClienteId;
        }

        await _agendamentoRepository.EditarAgendamento(agendamento, id);

        var agendamentoDTO = _mapper.Map<AgendamentoDTO>(agendamento);
        return agendamentoDTO;
    }

    public async Task ExcluirAgendamento(int id)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");

        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var agendamento = await _agendamentoRepository.BuscarAgendamentoPorId(id);

        if (agendamento is null)
            throw new Exception("Agendamento não encontrado");

        if (agendamento.ClienteId != usuario.Id && !usuario.IsAdmin)
            throw new ApplicationException("Você não pode deletar agendamento de outro usuário");

        await _agendamentoRepository.ExcluirAgendamento(id);
    }

    public async Task<List<AgendamentoDTO>> ListarAgendamentos()
    {
        var agendamentos = await _agendamentoRepository.ListarAgendamentos();
        var agendamentosDTO = _mapper.Map<List<AgendamentoDTO>>(agendamentos);

        return agendamentosDTO;
    }

    public async Task<List<AgendamentoDTO>> ListarAgendamentosPorUsuario()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");

        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario is null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        var agendamentos = await _agendamentoRepository.ListarAgendamentosPorUsuario(usuario.Id);
        var agendamentosDTO = _mapper.Map<List<AgendamentoDTO>>(agendamentos);

        return agendamentosDTO;
    }
    
    private async Task<bool> DataHoraJaReservada(DateTime dataHora, int? ignorarId = null)
    {
        return await _agendamentoRepository.DataHoraJaReservada(dataHora, ignorarId);
    }
}
