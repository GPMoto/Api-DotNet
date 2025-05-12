using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Cidade> Cidade { get; set; }

        public DbSet<Estado> Estado { get; set; }

        public DbSet<Moto> Moto { get; set; }

        public DbSet<Perfil> Perfil { get; set; }

        public DbSet<TipoMoto> TipoMoto { get; set; }

        public DbSet<Pais> Pais { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Contato> Contato { get; set; }

        public DbSet<Uwb> Uwb { get; set; }

        public DbSet<TipoSecao> TipoSecao { get; set; }

        public DbSet<Telefone> Telefone { get; set; }

        public DbSet<Endereco> Endereco { get; set; }

        public DbSet<Filial> Filial { get; set; }

        public DbSet<SecoesFilial> SecoesFilial { get; set; }


    }
}
