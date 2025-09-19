using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Moto
    {
        /// <summary>
        /// Identificador único da moto.
        /// </summary>
        public int id_moto { get; set; }

        /// <summary>
        /// Status da moto (0 = inativa, 1 = ativa).
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Condição da moto (ex: Nova, Usada).
        /// </summary>
        public string CondicaoMoto { get; set; }

        /// <summary>
        /// Identificador da moto (placa, chassi ou número do motor).
        /// </summary>
        public string IdentificadorMoto { get; set; }

        /// <summary>
        /// Identificador da filial associada à moto.
        /// </summary>
        public int id_filial { get; set; }

        /// <summary>
        /// Identificador do tipo da moto.
        /// </summary>
        public int id_tipo_moto { get; set; }


    }
}
