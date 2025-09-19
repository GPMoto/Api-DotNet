using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Contato
    {

        /// <summary>
        /// Identificador único do contato.
        /// </summary>
        public int id_contato { get; set; }

        /// <summary>
        /// Nome do dono do contato.
        /// </summary>
        public string nmDono { get; set; }

        /// <summary>
        /// Status do contato (0 = inativo, 1 = ativo).
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// Identificador do telefone associado ao contato.
        /// </summary>
        public int id_Telefone { get; set; }
    }
}
