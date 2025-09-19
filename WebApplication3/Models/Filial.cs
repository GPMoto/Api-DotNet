using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Filial
    {

        /// <summary>
        /// Identificador único da filial.
        /// </summary>
        public int id_filial { get; set; }

        /// <summary>
        /// CNPJ da filial.
        /// </summary>
        public string Cnpj { get; set; }

        /// <summary>
        /// Senha da filial.
        /// </summary>
        public string senha { get; set; }

        /// <summary>
        /// Identificador do endereço associado à filial.
        /// </summary>
        public int id_endereco { get; set; }

        /// <summary>
        /// Identificador do contato associado à filial.
        /// </summary>
        public int id_contato { get; set; }


    }
}
