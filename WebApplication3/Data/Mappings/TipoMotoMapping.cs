using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TipoMotoMapping : IEntityTypeConfiguration<TipoMoto>
    {
        public void Configure(EntityTypeBuilder<TipoMoto> builder)
        {
            builder.ToTable("t_gpsMottu_tipo_moto");
            builder.HasKey(t => t.id_tipo_moto);
            builder.Property(t => t.id_tipo_moto)
                .HasColumnName("id_tipo_moto")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(t => t.NomeTipoMoto)
                .HasColumnName("nm_tipo_moto")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
    
    
}
