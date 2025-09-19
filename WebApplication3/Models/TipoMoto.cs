using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class TipoMoto
    {
        /// <summary>
        /// Identificador único do tipo de moto.
        /// </summary>
        public int id_tipo_moto { get; set; }

        /// <summary>
        /// Nome do tipo de moto.
        /// </summary>
        public string NomeTipoMoto { get; set; }
    }
}
