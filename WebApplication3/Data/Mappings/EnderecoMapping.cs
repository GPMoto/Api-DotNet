using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("T_GPMOTTU_ENDERECO");
            builder.HasKey(e => e.id_endereco);
            builder.Property(e => e.id_endereco)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_ENDERECO");
            builder.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("NR_CEP");
            builder.Property(e => e.NomeLogradouro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NM_LOGRADOURO");
            builder.Property(e => e.NumeroLogradouro)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("NR_LOGRADOURO");
            builder.Property(e => e.id_cidade)
                .IsRequired()
                .HasColumnName("ID_CIDADE");
            builder.HasOne<Cidade>()
                .WithMany()
                .HasForeignKey(e=>e.id_cidade)
                .HasConstraintName("T_GPMOTTU_CIDADE_FK");
        }
    }
}

