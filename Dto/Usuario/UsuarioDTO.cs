namespace AgendamentosApi.Dto.Usuario;

public class UsuarioDTO
{
    public required string Nome { get; set; }

    public required DateTime DataNascimento { get; set; }

    public required string Telefone { get; set; }

    public required string Email { get; set; }
}
