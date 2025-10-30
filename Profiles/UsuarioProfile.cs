using AgendamentosApi.Dto.Usuario;
using AgendamentosApi.Models;
using AutoMapper;

namespace AgendamentosApi.Profiles;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<CadastroDTO, Usuario>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Telefone));

        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src => src.PhoneNumber));

        CreateMap<EditarPerfilDTO, Usuario>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Telefone));
    }
}

/*
    ---- Configurações do AutoMapper ----

    A função CreateMap<Origem, Destino> define um mapeamento entre dois tipos de objetos.
    O AutoMapper copiará automaticamente as propriedades com o mesmo nome e tipo.
    Quando os nomes diferirem, usamos o método .ForMember() para configurar manualmente.
    Essa configuração manual colocamos na mesma ordem de declaração do CreateMap, onde explicitamos
    a origem do mapeamento e o destino dele.

    -------------------------------------
*/