namespace WebApplication3.Exceptions
{
    public class TamanhoInvalidoException : Exception
    {
        public TamanhoInvalidoException(int min,int max,string name) : base($"O tamanho maximo de {name} é de {max} e minimo de {min}.") { }
        public TamanhoInvalidoException(int max,string name) : base($"O tamanho maximo de {name} é de {max}.") { }
    }
}
