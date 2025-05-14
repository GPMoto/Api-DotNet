using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication3.Models;

namespace WebApplication3.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("t_gpsMottu_usuario");
            builder.HasKey(u => u.id_usuario);
            builder.Property(u => u.id_usuario)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_usuario");
            builder.Property(u => u.NomeUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("nm_usuario");
            builder.Property(u => u.SenhaUsuario)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("ds_senha");
            builder.Property(u => u.EmailUsuario)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("ds_email");
            builder.HasOne<Perfil>()
                .WithMany()
                .HasForeignKey(u => u.id_perfil)
                .HasConstraintName("usuario_perfil");
            builder.HasOne<Filial>()
                .WithMany()
                .HasForeignKey(u => u.id_filial)
                .HasConstraintName("usuario_filial");
        }
    }
    
}
