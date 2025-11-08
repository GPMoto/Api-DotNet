using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class FilialMapping : IEntityTypeConfiguration<Filial>
    {

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Filial> builder)
        {
            builder.ToTable("T_GPMOTTU_FILIAL");
            builder.HasKey(f => f.id_filial);
            builder.Property(f => f.Cnpj)
                .IsRequired()
                .HasColumnName("CNPJ_FILIAL")
                .HasMaxLength(14);
            builder.Property(f => f.senha)
                .IsRequired()
                .HasColumnName("SENHA_FILIAL")
                .HasMaxLength(200);
            builder.Property(f => f.id_endereco)
                .IsRequired()
                .HasColumnName("ID_ENDERECO");
            builder.Property(f => f.id_contato)
                .IsRequired()
                .HasColumnName("ID_CONTATO");
            builder.HasOne<Endereco>()
                .WithMany()
                .HasForeignKey(f => f.id_endereco)
                .HasConstraintName("T_GPMOTTU_ENDERECO_FK");
            builder.HasOne<Contato>()
                .WithMany()
                .HasForeignKey(f=>f.id_contato)
                .HasConstraintName("T_GPMOTTU_CONTATO_FK");
        }
    }
}
