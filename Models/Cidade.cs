using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_cidade")]
    public class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_cidade { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para nome de cidade é 200 caractéres")]
        [Column("nm_cidade")]
        public string NomeCidade { get; set; }

        [ForeignKey(nameof(Estado))]
        [Column("id_estado")]
        public int id_estado { get; set; }

    }
}
