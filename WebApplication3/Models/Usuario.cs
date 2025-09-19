using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Usuario
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public int id_usuario { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string NomeUsuario { get; set; }

        /// <summary>
        /// Email do usuário.
        /// </summary>
        public string EmailUsuario { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string SenhaUsuario { get; set; }

        /// <summary>
        /// Identificador do perfil associado ao usuário.
        /// </summary>
        public int id_perfil { get; set; }

        /// <summary>
        /// Identificador da filial associada ao usuário.
        /// </summary>
        public int id_filial { get; set; }
    }
}
