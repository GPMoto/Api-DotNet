using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {

        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("t_gpsMottu_cidade");
            builder.HasKey(c => c.id_cidade);
            builder.Property(c => c.id_cidade)
                .HasColumnName("id_cidade")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.NomeCidade)
                .HasColumnName("nm_cidade")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.id_estado)
                .HasColumnName("id_estado")
                .IsRequired();
            builder.HasOne<Estado>()
                .WithMany()
                .HasForeignKey(c => c.id_estado)
                .HasConstraintName("cidade_estado");
        }
    }
}
