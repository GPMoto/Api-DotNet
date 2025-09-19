using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Cidade
    {

        /// <summary>
        /// Identificador único da cidade.
        /// </summary>
        public int id_cidade { get; set; }

        /// <summary>
        /// Nome da cidade.
        /// </summary>
        public string NomeCidade { get; set; }

        /// <summary>
        /// Identificador do estado ao qual a cidade pertence.
        /// </summary>
        public int id_estado { get; set; }

    }
}
