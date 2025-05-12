using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    [Table("t_gpsMottu_moto")]
    public class Moto
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_moto { get; set; }

        [Required]
        [Range(1,1, ErrorMessage ="Valor para status é somente 1 e 0")]
        public int Status { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Tamanho maximo para condições de manutenção da moto é 200 caractéres")]
        [Column("condicao_manutencao_moto")]
        public string CondicaoMoto { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Tamanho maximo para identificador da moto é 50 caractéres")]
        [Column("identificador_moto")]
        public string IdentificadorMoto { get; set; }

        [ForeignKey(nameof(Filial))]
        [Column("id_filial")]
        public int id_filial { get; set; }

        [ForeignKey(nameof(TipoMoto))]
        [Column("id_tipo_moto")]
        public int id_tipo_moto { get; set; }

    }
}
