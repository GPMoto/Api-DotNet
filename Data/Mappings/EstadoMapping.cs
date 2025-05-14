using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.ToTable("t_gpsMottu_estado");
            builder.HasKey(e => e.id_estado);
            builder.Property(e => e.id_estado)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_estado");
            builder.Property(e => e.NomeEstado)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nm_estado");
            builder.HasOne<Pais>()
                .WithMany()
                .HasForeignKey(e => e.id_pais)
                .HasConstraintName("estado_pais");

        }
    }
}
