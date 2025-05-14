namespace WebApplication3.Exceptions
{
    public class CnpjTamanhoInvalidoException : Exception
    {
        public CnpjTamanhoInvalidoException() : base("O tamanho de cnpj é de 14") { }
    }
}
