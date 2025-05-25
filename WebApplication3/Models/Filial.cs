using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication3.Models
{

    public class Filial
    {

        public int id_filial { get; set; }


        public string Cnpj { get; set; }


        public string senha { get; set; }


        public int id_endereco { get; set; }


        public int id_contato { get; set; }


    }
}
