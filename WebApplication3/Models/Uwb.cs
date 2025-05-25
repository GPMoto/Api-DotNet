using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Uwb
    {


        public int id_uwb { get; set; }

        public int id_moto { get; set; }

        public string ValorUwb { get; set; }
    }
}
