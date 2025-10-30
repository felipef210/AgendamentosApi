using AgendamentosApi.Dto.Agendamento;
using AgendamentosApi.Models;
using AutoMapper;

namespace AgendamentosApi.Profiles;

public class AgendamentoProfile : Profile
{
    public AgendamentoProfile()
    {
        CreateMap<CriarAgendamentoDTO, Agendamento>();
        CreateMap<Agendamento, AgendamentoDTO>()
            .ForMember(dest => dest.ClienteNome, opt => opt.MapFrom(src => src.Cliente.Nome))
            .ForMember(dest => dest.ClienteEmail, opt => opt.MapFrom(src => src.Cliente.Email))
            .ForMember(dest => dest.ClienteTelefone, opt => opt.MapFrom(src => src.Cliente.PhoneNumber));
    }
}