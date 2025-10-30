using AgendamentosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgendamentosApi.Data;

public class Context : IdentityDbContext<Usuario>
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
}