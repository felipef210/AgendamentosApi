using UsuarioModel = AgendamentosApi.Models.Usuario;

namespace AgendamentosApi.Services.Token;

public interface ITokenService
{
    string GerarToken(UsuarioModel usuario);
}