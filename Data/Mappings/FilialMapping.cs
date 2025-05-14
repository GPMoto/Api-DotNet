using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class FilialMapping : IEntityTypeConfiguration<Filial>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Filial> builder)
        {
            builder.ToTable("t_gpsMottu_filial");
            builder.HasKey(f => f.id_filial);
            builder.Property(f => f.Cnpj)
                .IsRequired()
                .HasColumnName("cnpj_filial")
                .HasMaxLength(14);
            builder.Property(f => f.senha)
                .IsRequired()
                .HasColumnName("senha_filial")
                .HasMaxLength(200);
            builder.HasOne<Endereco>()
                .WithMany()
                .HasForeignKey(f => f.id_endereco)
                .HasConstraintName("filial_endereco");
            builder.HasOne<Contato>()
                .WithMany()
                .HasForeignKey(f=>f.id_contato)
                .HasConstraintName("fk_contato_filial");
        }
    }
}
