using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class TipoSecao
    {

        /// <summary>
        /// Identificador único do tipo de seção de filial.
        /// </summary>
        public int id_tipo_secao { get; set; }

        /// <summary>
        /// Nome do tipo de seção de filial.
        /// </summary>
        public string NomeTipoSecao { get; set; }
    }
}
