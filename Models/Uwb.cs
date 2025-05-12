using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_uwb")]
    public class Uwb
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_uwb { get; set; }

        [ForeignKey(nameof(Moto))]
        [Column("id_moto")]
        public Moto id_moto { get; set; }


        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para nome de UWB é 200 caractéres")]
        public string ValorUwb { get; set; }
    }
}
