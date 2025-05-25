using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class SecoesFilialMapping : IEntityTypeConfiguration<SecoesFilial>
    {
        public void Configure(EntityTypeBuilder<SecoesFilial> builder)
        {
            builder.ToTable("t_gpsMottu_secoes_filial");
            builder.HasKey(s => s.id_secao);
            builder.Property(s => s.id_secao)
                .HasColumnName("id_secao_filial")
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Lado1)
                .HasColumnName("Lado_1")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado2)
                .HasColumnName("Lado_2")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado3)
                .HasColumnName("Lado_3")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado4)
                .HasColumnName("Lado_4")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.HasOne<TipoSecao>()
                .WithMany()
                .HasForeignKey(s => s.id_tipo_secao)
                .HasConstraintName("secao_tipoSecao")
                .IsRequired();
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(s => s.id_filial)
                .IsRequired()
                .HasConstraintName("secao_filial");
        }
    }
    
}

