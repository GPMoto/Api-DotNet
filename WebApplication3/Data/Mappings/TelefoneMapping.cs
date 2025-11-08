using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("T_GPMOTTU_TELEFONE");
            builder.HasKey(t => t.id_telefone);
            builder.Property(t => t.id_telefone)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_TELEFONE");
            builder.Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("NR_TELEFONE");
            builder.Property(t => t.Ddd)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("NR_DDD");
            builder.Property(t => t.Ddi)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("NR_DDI");
            builder.Property(t => t.id_contato)
                .IsRequired()
                .HasColumnName("ID_CONTATO");
            builder.HasOne<Contato>()
                .WithOne()
                .HasForeignKey<Contato>(c => c.id_Telefone)
                .HasConstraintName("TELEFONE_CONTATO_FK");

        }
    }
    
    
}
