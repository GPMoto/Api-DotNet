using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Contato
    {

        public int id_contato { get; set; }

        public string nmDono { get; set; }


        public int status { get; set; }

        public int id_Telefone { get; set; }
    }
}
