using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class ContatoMapping : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("t_gpsMottu_contato");
            builder.HasKey(c => c.id_contato);
            builder.Property(c => c.id_contato)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_contato");
            builder.Property(c => c.nmDono)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("nm_dono");
            builder.HasOne<Contato>()
                .WithOne()
                .HasForeignKey<Contato>(t => t.id_Telefone)
                .HasConstraintName("Contato_telefone_fk");
        }
    }  
    
}
