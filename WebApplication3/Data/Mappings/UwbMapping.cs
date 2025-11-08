using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class UwbMapping : IEntityTypeConfiguration<Uwb>
    {
        public void Configure(EntityTypeBuilder<Uwb> builder)
        {
            builder.ToTable("T_GPMOTTU_UWB");
            builder.HasKey(u => u.id_uwb);
            builder.Property(u => u.id_uwb)
                .HasColumnName("ID_UWB")
                .ValueGeneratedOnAdd();
            builder.HasOne<Moto>()
                .WithMany()
                .HasForeignKey(u => u.id_moto);
            builder.Property(u => u.ValorUwb)
                .HasColumnName("VL_UWB")
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(u => u.id_moto)
                .HasColumnName("ID_MOTO")
                .IsRequired();
        }
    }
}
