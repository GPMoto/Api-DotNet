using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Perfil
    {

        /// <summary>
        /// Identificador único do perfil.
        /// </summary>
        public int id_perfil { get; set; }

        /// <summary>
        /// Nome do perfil.
        /// </summary>
        public string NomePerfil { get; set; }
    }
}
