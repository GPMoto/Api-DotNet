using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Usuario
    {
        public int id_usuario { get; set; }


        public string NomeUsuario { get; set; }

        public string EmailUsuario { get; set; }


        public string SenhaUsuario { get; set; }

        public int id_perfil { get; set; }


        public int id_filial { get; set; }
    }
}
