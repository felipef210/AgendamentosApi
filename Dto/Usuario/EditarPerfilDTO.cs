using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Dto.Usuario;

public class EditarPerfilDTO
{
    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [DataType(DataType.PhoneNumber)]
    public required string Telefone { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
}
