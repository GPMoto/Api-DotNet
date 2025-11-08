using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Uwb
    {

        /// <summary>
        /// Identificador único do UWB.
        /// </summary>
        public int id_uwb { get; set; }

        /// <summary>
        /// Identificador da moto associada ao UWB.
        /// </summary>
        public int id_moto { get; set; }

        /// <summary>
        /// Valor do UWB.
        /// </summary>
        public string ValorUwb { get; set; }

    }
}
