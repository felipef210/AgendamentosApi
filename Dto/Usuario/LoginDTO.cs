using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Dto.Usuario;

public class LoginDTO
{
    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required string Senha { get; set; }
}
