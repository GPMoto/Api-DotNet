using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class UwbMapping : IEntityTypeConfiguration<Uwb>
    {
        public void Configure(EntityTypeBuilder<Uwb> builder)
        {
            builder.ToTable("t_gpsMottu_uwb");
            builder.HasKey(u => u.id_uwb);
            builder.Property(u => u.id_uwb)
                .HasColumnName("id_uwb")
                .ValueGeneratedOnAdd();
            builder.HasOne<Moto>()
                .WithMany()
                .HasForeignKey(u => u.id_moto)
                .IsRequired();
            builder.Property(u => u.ValorUwb)
                .HasColumnName("vl_iwb")
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
