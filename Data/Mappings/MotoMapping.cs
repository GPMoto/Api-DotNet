using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure (EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("t_gpsMottu_moto");
            builder.HasKey(e => e.id_moto);
            builder.Property(e => e.id_moto)
                .HasColumnName("id_moto")
                .ValueGeneratedOnAdd();
            builder.HasOne<TipoMoto>()
                .WithMany()
                .HasForeignKey(e => e.id_tipo_moto)
                .HasConstraintName("FK_Moto_TipoMoto");
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(e => e.id_filial)
                .HasConstraintName("FK_Moto_Filial");
            builder.Property(e => e.Status)
                .HasColumnName("status")
                .IsRequired();
            builder.Property(e => e.CondicaoMoto)
                .HasColumnName("condicao_manutencao_moto")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(e => e.IdentificadorMoto)
                .HasColumnName("identificador_moto")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
