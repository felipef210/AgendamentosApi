using AgendamentosApi.Data;
using UsuarioModel = AgendamentosApi.Models.Usuario;
using Microsoft.EntityFrameworkCore;

namespace AgendamentosApi.Repository.Usuario;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly Context _context;

    public UsuarioRepository(Context context)
    {
        _context = context;
    }

    public async Task<UsuarioModel> BuscarUsuarioPorId(string id)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (usuario is null)
            throw new NaoEncontradoException("Usuário não encontrado");

        return usuario;
    }

    public async Task EditarUsuario(UsuarioModel usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExiste(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email!.Equals(email));
    }

    public async Task<List<UsuarioModel>> ListarUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return usuarios;
    }
}
