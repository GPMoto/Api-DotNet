using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_usuario")]
    public class Usuario
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_usuario { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para nome de usuario é 200 caractéres")]
        [Column("nm_usuario")]
        public string NomeUsuario { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para email de usuario é 200 caractéres")]
        [Column("nm_email")]
        public string EmailUsuario { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para senha de usuario é 200 caractéres")]
        [Column("nm_senha")]
        public string SenhaUsuario { get; set; }

        [ForeignKey(nameof(Perfil))]
        public Perfil id_perfil { get; set; }

        [ForeignKey(nameof(Filial))]
        public int id_filial { get; set; }
    }
}
