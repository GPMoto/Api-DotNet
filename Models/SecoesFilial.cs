using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_Secoes_Filial")]
    public class SecoesFilial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_secao { get; set; }

        [Required]
        [Range(1,100000, ErrorMessage = "Valor máximo de lados é 100000")]
        [Column("Lado_1")]
        public int Lado1 { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Valor máximo de lados é 100000")]
        [Column("Lado_2")]
        public int Lado2 { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Valor máximo de lados é 100000")]
        [Column("Lado_3")]
        public int Lado3 { get; set; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Valor máximo de lados é 100000")]
        [Column("Lado_4")]
        public int Lado4 { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Tamanho maximo para nome de seção é 100 caractéres")]
        [Column("nm_secao")]
        public string NomeSecao { get; set; }

        [ForeignKey(nameof(TipoSecao))]
        [Column("id_tipo_secao")]
        public TipoSecao id_tipo_secao { get; set; }

        [ForeignKey(nameof(Filial))]
        public int id_filial { get; set; }
    }
}
