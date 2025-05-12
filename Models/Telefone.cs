using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    [Table("t_gpsMottu_telefone")]
    public class Telefone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_telefone { get; set; }

        [Required]
        [StringLength(3, ErrorMessage ="Número maximo de caracteres no DDD é de 3")]
        public string Ddd { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Número maximo de caracteres no DDI é de 3")]
        public string Ddi { get; set; }

        [Required]
        [StringLength(0, ErrorMessage = "Número maximo de caracteres no telefone é de 9")]
        [Column("nr_telefone")]
        public string Numero { get; set; }


    }
}
