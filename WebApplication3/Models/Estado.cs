using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Estado
    {
        /// <summary>
        /// Identificador único do estado.
        /// </summary>
        public int id_estado { get; set; }

        /// <summary>
        /// Nome do estado.
        /// </summary>
        public string NomeEstado { get; set; }

        /// <summary>
        /// Identificador do país associado ao estado.
        /// </summary>
        public int id_pais { get; set; }

    }
}
