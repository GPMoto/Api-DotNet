using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_tipo_moto")]
    public class TipoMoto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_tipo_moto { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Tamanho maximo para nome de tipo de moto é 50 caractéres")]
        [Column("nm_tipo_moto")]
        public string NomeTipoMoto { get; set; }
    }
}
