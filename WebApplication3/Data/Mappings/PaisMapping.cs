using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class PaisMapping : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("T_GPMOTTU_PAIS");
            builder.HasKey(p => p.Id_pais);
            builder.Property(p => p.Id_pais)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(p => p.NomePais)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NM_PAIS");
        }
    }
}
