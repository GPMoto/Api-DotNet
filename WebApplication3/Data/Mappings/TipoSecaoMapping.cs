using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TipoSecaoMapping : IEntityTypeConfiguration<TipoSecao>
    {
        public void Configure(EntityTypeBuilder<TipoSecao> builder)
        {
            builder.ToTable("T_GPMOTTU_TIPO_SECAO");
            builder.HasKey(t => t.id_tipo_secao);
            builder.Property(t => t.id_tipo_secao)
                .HasColumnName("ID_TIPO_SECAO")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(t => t.NomeTipoSecao)
                .HasColumnName("NM_SECAO")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
