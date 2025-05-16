namespace WebApplication3.Exceptions
{
    public class TamanhoInvalidoException : Exception
    {
        public TamanhoInvalidoException(int min,int max) : base($"O tamanho maximo de lado é de {max} e minimo de {min}.") { }
    }
}
