using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Endereco
    {

        /// <summary>
        /// Identificador único do endereço.
        /// </summary>
        public int id_endereco { get; set; }
        
        /// <summary>
        /// Nome do logradouro.
        /// </summary>
        public string NomeLogradouro { get; set; }

        /// <summary>
        /// Número do logradouro.
        /// </summary>
        public string NumeroLogradouro { get; set; }

        /// <summary>
        /// CEP do endereço.
        /// </summary>
        public string Cep { get; set; }


        /// <summary>
        /// Identificador da cidade associada ao endereço.
        /// </summary>
        public int id_cidade { get; set; }
    }
}
