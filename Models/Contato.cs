using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    [Table("t_gpsMottu_contato")]
    public class Contato
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_contato { get; set; }

        [Required]
        [StringLength(200)]
        [Column("nm_dono")]
        public string nmDono { get; set; }

        [Required]
        [Range(1,1, ErrorMessage ="Status pode ser somente 0 e 1")]
        public int status { get; set; }


        [ForeignKey(nameof(Filial))]
        [Column("id_filial")]
        public Filial Filial { get; set; }


        [ForeignKey(nameof(Telefone))]
        [Column("id_telefone")]
        public Telefone Telefone { get; set; }
    }
}
