using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TipoMotoMapping : IEntityTypeConfiguration<TipoMoto>
    {
        public void Configure(EntityTypeBuilder<TipoMoto> builder)
        {
            builder.ToTable("T_GPMOTTU_TIPO_MOTO");
            builder.HasKey(t => t.id_tipo_moto);
            builder.Property(t => t.id_tipo_moto)
                .HasColumnName("ID_TIPO_MOTO")
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(t => t.NomeTipoMoto)
                .HasColumnName("NM_TIPO")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
    
    
}
