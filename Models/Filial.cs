using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    [Table("t_gpsMottu_filial")]
    public class Filial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1,100000,ErrorMessage = "Tamanho maximo de id igual à 100000")]
        public int id_filial { get; set; }

        [Required]
        [Column("cnpj_filial")]
        [MaxLength(14, ErrorMessage = "O CNPJ deve ter no máximo 14 caracteres.")]
        public string Cnpj;

        [Required]
        [Column("senha_filial")]
        [MaxLength(200, ErrorMessage = "Valor maximo de senha é de 200 caracteres")]
        public string senha;

        [Required]
        [ForeignKey(nameof(Endereco))]
        [Column("id_endereco")]
        public Endereco Endereco;

        [Required]
        [ForeignKey(nameof(Contato))]
        [Column("id_contato")]
        public Contato Contato;

    }
}
