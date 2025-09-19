using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class SecoesFilial
    {

        /// <summary>
        /// Identificador único da seção de filial.
        /// </summary>        
        public int id_secao { get; set; }

        /// <summary>
        /// Tamanho do lado 1 da seção.
        /// </summary>
        public int Lado1 { get; set; }

        /// <summary>
        /// Tamanho do lado 2 da seção.
        /// </summary>
        public int Lado2 { get; set; }

        /// <summary>
        /// Tamanho do lado 3 da seção.
        /// </summary>
        public int Lado3 { get; set; }

        /// <summary>
        /// Tamanho do lado 4 da seção.
        /// </summary>
        public int Lado4 { get; set; }

        /// <summary>
        /// Identificador do tipo de seção.
        /// </summary>
        public int id_tipo_secao { get; set; }

        /// <summary>
        /// Identificador da filial associada à seção.
        /// </summary>
        public int id_filial { get; set; }
    }
}
