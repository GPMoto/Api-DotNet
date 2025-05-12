using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_pais")]
    public class Pais
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_pais { get; set; }

        [Required]
        [MaxLength(100,ErrorMessage ="Tamanho maximo para nome de país é 100 caractéres")]
        [Column("nm_pais")]
        public string NomePais { get; set; }
    }
}
