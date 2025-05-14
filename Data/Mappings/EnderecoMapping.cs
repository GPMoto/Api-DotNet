using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("t_gpsMottu_endereco");
            builder.HasKey(e => e.id_endereco);
            builder.Property(e => e.id_endereco)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_endereco");
            builder.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("cep");
            builder.Property(e => e.NomeLogradouro)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("logradouro");
            builder.Property(e => e.NumeroLogradouro)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("numero");
            builder.Property(e => e.Cep)
                .IsRequired()
                .HasMaxLength(8)
                .HasColumnName("cep");
            builder.HasOne<Cidade>()
                .WithMany()
                .HasForeignKey(e=>e.id_cidade)
                .HasConstraintName("cidade_endereco");
        }
    }
}

