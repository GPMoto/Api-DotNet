using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{


    public class Estado
    {
        public int id_estado { get; set; }

        public string NomeEstado { get; set; }

        public int id_pais { get; set; }

    }
}
