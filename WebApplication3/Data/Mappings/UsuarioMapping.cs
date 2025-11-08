using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("T_GPMOTTU_USUARIO");
            builder.HasKey(u => u.id_usuario);
            builder.Property(u => u.id_usuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID_USUARIO");
            builder.Property(u => u.NomeUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NM_USUARIO");
            builder.Property(u => u.SenhaUsuario)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("SENHA");
            builder.Property(u => u.EmailUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("NM_EMAIL");
            builder.Property(u => u.id_filial)
                .HasColumnName("ID_FILIAL")
                .IsRequired();
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(u => u.id_filial)
                .HasConstraintName("USUARIO_FILIAL_FK");
        }
    }
    
}
