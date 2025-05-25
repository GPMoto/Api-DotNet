using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Endereco
    {

        public int id_endereco { get; set; }
        

        public string NomeLogradouro { get; set; }

        public string NumeroLogradouro { get; set; }

        public string Cep { get; set; }

        public int id_cidade { get; set; }
    }
}
