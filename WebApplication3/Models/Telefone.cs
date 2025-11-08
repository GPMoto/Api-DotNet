using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Telefone
    {
        /// <summary>
        /// Identificador único do telefone.
        /// </summary>
        public int id_telefone { get; set; }

        /// <summary>
        /// DDD do telefone (3 dígitos).
        /// </summary>
        public string Ddd { get; set; }

        /// <summary>
        /// DDI do telefone (3 dígitos).
        /// </summary>
        public string Ddi { get; set; }

        /// <summary>
        /// Número do telefone.
        /// </summary>
        public string Numero { get; set; }


        /// <summary>
        ///  ID do contato associado a este telefone.
        /// </summary>
        public int id_contato { get; set; }


    }
}
