using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Telefone
    {

        public int id_telefone { get; set; }

        public string Ddd { get; set; }


        public string Ddi { get; set; }


        public string Numero { get; set; }


    }
}
