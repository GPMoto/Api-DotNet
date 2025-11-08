using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure (EntityTypeBuilder<Moto> builder)
        {
            builder.ToTable("T_GPMOTTU_MOTO");
            builder.HasKey(e => e.id_moto);
            builder.Property(e => e.id_moto)
                .HasColumnName("ID_MOTO")
                .ValueGeneratedOnAdd();
            builder.Property(e => e.id_tipo_moto)
                .HasColumnName("ID_TIPO_MOTO")
                .IsRequired();
            builder.Property(e => e.id_filial)
                .HasColumnName("ID_FILIAL")
                .IsRequired();
            builder.HasOne<TipoMoto>()
                .WithMany()
                .HasForeignKey(e => e.id_tipo_moto)
                .HasConstraintName("T_GPMOTTU_TIPO_MOTO_FK");
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(e => e.id_filial)
                .HasConstraintName("MOTO_FILIAL_FK");
            builder.Property(e => e.Status)
                .HasColumnName("STATUS")
                .IsRequired();
            builder.Property(e => e.CondicaoMoto)
                .HasColumnName("CONDICOES_MANUTENCAO")
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(e => e.IdentificadorMoto)
                .HasColumnName("IDENTIFICADOR")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
