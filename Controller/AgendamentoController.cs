using AgendamentosApi.Dto.Agendamento;
using AgendamentosApi.Services.Agendamento;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendamentosApi.Controller;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentoController(IAgendamentoService  agendamentoService)
    {
         _agendamentoService = agendamentoService;
    }

    [HttpGet("listar")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<List<AgendamentoDTO>>> ListarAgendamentos()
    {
        var agendamentosDTO = await _agendamentoService.ListarAgendamentos();
        return Ok(agendamentosDTO);
    }

    [HttpGet("listarPorUsuario")]
    public async Task<ActionResult<List<AgendamentoDTO>>> ListarAgendamentosPorUsuario()
    {
        var agendamentosDTO = await _agendamentoService.ListarAgendamentosPorUsuario();
        return Ok(agendamentosDTO);
    }

    [HttpGet("agendamento/{id}")]
    public async Task<ActionResult<AgendamentoDTO>> BuscarAgendamentoPorId(int id)
    {
        var agendamentoDTO = await _agendamentoService.BuscarAgendamentoPorId(id);
        return Ok(agendamentoDTO);
    }

    [HttpPost]
    public async Task<ActionResult<AgendamentoDTO>> CriarAgendamento([FromBody] CriarAgendamentoDTO criarAgendamentoDTO)
    {
        var agendamentoDTO = await _agendamentoService.CriarAgendamento(criarAgendamentoDTO);
        return Ok(agendamentoDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AgendamentoDTO>> EditarAgendamento([FromBody] CriarAgendamentoDTO editarAgendamentoDTO, int id)
    {
        var agendamentoDTO = await _agendamentoService.EditarAgendamento(editarAgendamentoDTO, id);
        return Ok(agendamentoDTO);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> ExcluirAgendamento(int id)
    {
        await _agendamentoService.ExcluirAgendamento(id);
        return NoContent();
    }
}
