using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class ContatoMapping : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("T_GPMOTTU_CONTATO");
            builder.HasKey(c => c.id_contato);
            builder.Property(c => c.id_contato)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_CONTATO");
            builder.Property(c => c.nmDono)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("NM_DONO");
            builder.Property(c => c.status)
                .IsRequired()
                .HasColumnName("STATUS");
            builder.Property(c => c.id_Telefone)
                .IsRequired()
                .HasColumnName("ID_TELEFONE");
            builder.HasOne<Contato>()
                .WithOne()
                .HasForeignKey<Contato>(t => t.id_Telefone)
                .HasConstraintName("Contato_telefone_fk");
        }
    }  
    
}
