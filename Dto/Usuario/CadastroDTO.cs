using System.ComponentModel.DataAnnotations;

namespace AgendamentosApi.Dto.Usuario;

public class CadastroDTO
{
    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [DataType(DataType.PhoneNumber)]
    public required string Telefone { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [DataType(DataType.Password)]
    public required string Senha { get; set; }

    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    [Compare("Senha", ErrorMessage = "As senhas devem coincidir")]
    [DataType(DataType.Password)]
    public required string ConfirmarSenha { get; set; }
}
