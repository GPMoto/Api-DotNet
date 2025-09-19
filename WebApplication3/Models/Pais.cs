using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Pais
    {

        /// <summary>
        /// Identificador único do país.
        /// </summary>
        public int Id_pais { get; set; }

        /// <summary>
        /// Nome do país.
        /// </summary>
        public string NomePais { get; set; }
    }
}
