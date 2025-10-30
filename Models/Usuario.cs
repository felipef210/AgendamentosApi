using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AgendamentosApi.Models;

public class Usuario : IdentityUser
{
    [Required(ErrorMessage = "Você deve preencher o campo {0}")]
    public required string Nome { get; set; }

    
    public DateTime DataNascimento { get; set; }

    public bool IsAdmin { get; set; } = false;
    public Usuario() : base() { }

    [JsonIgnore]
    public ICollection<Agendamento>? Agendamentos { get; set; }
}