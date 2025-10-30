using UsuarioModel = AgendamentosApi.Models.Usuario;

namespace AgendamentosApi.Repository.Usuario;

public interface IUsuarioRepository
{
    Task<List<UsuarioModel>> ListarUsuarios();
    Task<UsuarioModel> BuscarUsuarioPorId(string id);
    Task EditarUsuario(UsuarioModel usuario);
    Task<bool> EmailExiste(string email);
}
