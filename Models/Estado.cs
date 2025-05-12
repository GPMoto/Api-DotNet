using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_estado")]
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_estado { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho maximo para nome de estado é 100 caractéres")]
        [Column("nm_estado")]
        public string NomeEstado { get; set; }

        [ForeignKey(nameof(Pais))]
        [Column("id_pais")]
        public int id_pais { get; set; }

    }
}
