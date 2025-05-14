using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{
    public class Moto
    {
        public int id_moto { get; set; }

        public int Status { get; set; }

        public string CondicaoMoto { get; set; }

        public string IdentificadorMoto { get; set; }


        public int id_filial { get; set; }

        public int id_tipo_moto { get; set; }


    }
}
