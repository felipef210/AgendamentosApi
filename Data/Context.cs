using AgendamentosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AgendamentosApi.Data;

public class Context : IdentityDbContext<Usuario>
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Servico>().HasData(
            new Servico { Id = 1, Nome = "Maquiagem", DuracaoEmMinutos = 60 },
            new Servico { Id = 2, Nome = "Penteado", DuracaoEmMinutos = 120 },
            new Servico { Id = 3, Nome = "Sobrancelha", DuracaoEmMinutos = 40 },
            new Servico { Id = 4, Nome = "Curso", DuracaoEmMinutos = 180 }
        );
    } 

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Servico> Servicos { get; set; }
}