using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_perfil")]
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_perfil { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para nome de perfil é 200 caractéres")]
        [Column("nm_perfil")]
        public string NomePerfil { get; set; }
    }
}
