using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("t_gpsMottu_perfil");
            builder.HasKey(p => p.id_perfil);
            builder.Property(p => p.id_perfil)
                .HasColumnName("id_perfil")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.NomePerfil)
                .HasColumnName("nm_perfil")
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
