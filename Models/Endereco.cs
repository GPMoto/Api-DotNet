using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    [Table("t_gpsMottu_endereco")]
    public class Endereco
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_endereco { get; set; }
        
        [Required]
        [StringLength(200, ErrorMessage ="Tamanho máximo para nome de logradouro igual a 200")]
        [Column("nm_logradouro")]
        public string NomeLogradouro { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "Tamanho máximo para número de logradouro igual a 8")]
        [Column("nr_logradouro")]
        public string NumeroLogradouro { get; set; }

        [Required]
        [StringLength(8,ErrorMessage ="Tamanho máximo para CEP igual a 8")]
        [Column("nr_cep")]
        public string Cep { get; set; }

        [ForeignKey(nameof(Filial))]
        public int id_filial { get; set; }

        [ForeignKey(nameof(Cidade))]
        public int id_cidade { get; set; }
    }
}
