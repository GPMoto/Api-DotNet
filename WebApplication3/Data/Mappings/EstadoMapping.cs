using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("T_GPMOTTU_ESTADO");
            builder.HasKey(e => e.id_estado);
            builder.Property(e => e.id_estado)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_ESTADO");
            builder.Property(e => e.NomeEstado)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("NM_ESTADO");
            builder.Property(e => e.id_pais)
                .IsRequired()
                .HasColumnName("ID_PAIS");
            builder.HasOne<Pais>()
                .WithMany()
                .HasForeignKey(e => e.id_pais)
                .HasConstraintName("T_GPMOTTU_PAIS_FK");

        }
    }
}
