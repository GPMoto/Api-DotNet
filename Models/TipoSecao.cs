using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_tipo_secao")]
    public class TipoSecao
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_tipo_secao { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para nome de tipo de seção é 200 caractéres")]
        [Column("nm_tipo_secao")]
        public string NomeTipoSecao { get; set; }
    }
}
