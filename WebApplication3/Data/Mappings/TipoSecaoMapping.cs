using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TipoSecaoMapping : IEntityTypeConfiguration<TipoSecao>
    {
        public void Configure(EntityTypeBuilder<TipoSecao> builder)
        {
            builder.ToTable("t_gpsMottu_tipo_secao");
            builder.HasKey(t => t.id_tipo_secao);
            builder.Property(t => t.id_tipo_secao)
                .HasColumnName("id_tipo_secao")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(t => t.NomeTipoSecao)
                .HasColumnName("nm_tipo_secao")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
