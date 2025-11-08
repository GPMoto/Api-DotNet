using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class SecoesFilialMapping : IEntityTypeConfiguration<SecoesFilial>
    {
        public void Configure(EntityTypeBuilder<SecoesFilial> builder)
        {
            builder.ToTable("t_gpMottu_secoes_filial");
            builder.HasKey(s => s.id_secao);
            builder.Property(s => s.id_secao)
                .HasColumnName("ID_SECAO")
                .ValueGeneratedOnAdd();
            builder.Property(s => s.Lado1)
                .HasColumnName("LADO_1")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado2)
                .HasColumnName("LADO_2")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado3)
                .HasColumnName("LADO_3")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.Lado4)
                .HasColumnName("LADO_4")
                .IsRequired()
                .HasMaxLength(100000)
                .HasDefaultValue(0);
            builder.Property(s => s.id_tipo_secao)
                .HasColumnName("ID_TIPO_SECAO")
                .IsRequired();
            builder.Property(s => s.id_filial)
                .HasColumnName("ID_FILIAL")
                .IsRequired();
            builder.HasOne<TipoSecao>()
                .WithMany()
                .HasForeignKey(s => s.id_tipo_secao)
                .HasConstraintName("T_GPMOTTU_TIPO_SECAO_FK")
                .IsRequired();
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(s => s.id_filial)
                .IsRequired()
                .HasConstraintName("T_GPMOTTU_FILIAL_SECOES_FK");
        }
    }
    
}

