using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class SecoesFilial
    {

        public int id_secao { get; set; }


        public int Lado1 { get; set; }


        public int Lado2 { get; set; }


        public int Lado3 { get; set; }


        public int Lado4 { get; set; }


        public int id_tipo_secao { get; set; }


        public int id_filial { get; set; }
    }
}
