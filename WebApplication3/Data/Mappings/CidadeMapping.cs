using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {

        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("T_GPMOTTU_CIDADE");
            builder.HasKey(c => c.id_cidade);
            builder.Property(c => c.id_cidade)
                .HasColumnName("ID_CIDADE")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.NomeCidade)
                .HasColumnName("NM_CIDADE")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(c => c.id_estado)
                .HasColumnName("ID_ESTADO")
                .IsRequired();
            builder.HasOne<Estado>()
                .WithMany()
                .HasForeignKey(c => c.id_estado)
                .HasConstraintName("T_GPMOTTU_ESTADO_FK");
        }
    }
}
