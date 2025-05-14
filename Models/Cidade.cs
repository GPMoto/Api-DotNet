using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Cidade
    {

        public int id_cidade { get; set; }


        public string NomeCidade { get; set; }

        public int id_estado { get; set; }

    }
}
