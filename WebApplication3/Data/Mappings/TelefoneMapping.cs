using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("t_gpsMottu_telefone");
            builder.HasKey(t => t.id_telefone);
            builder.Property(t => t.id_telefone)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_telefone");
            builder.Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("nr_telefone");
            builder.Property(t => t.Ddd)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("nr_ddd");
            builder.Property(t => t.Ddi)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("nr_ddi");

        }
    }
    
    
}
