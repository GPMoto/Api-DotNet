using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("T_GPMOTTU_PERFIL");
            builder.HasKey(p => p.id_perfil);
            builder.Property(p => p.id_perfil)
                .HasColumnName("ID_PERFIL")
                .ValueGeneratedOnAdd();
            builder.Property(p => p.NomePerfil)
                .HasColumnName("NM_PERFIL")
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
