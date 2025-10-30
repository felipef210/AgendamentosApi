using AgendamentosApi.Dto.Usuario;
using Microsoft.AspNetCore.Mvc;


namespace AgendamentosApi.Services.Usuario
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> ListarUsuarios();
        Task<UsuarioDTO> BuscarUsuarioPorId();
        Task<UsuarioDTO> EditarUsuario([FromBody] EditarPerfilDTO editarPerfilDTO);
        Task Cadastrar([FromBody] CadastroDTO cadastroDTO);
        Task<string> Logar(LoginDTO loginDTO);
    }
}