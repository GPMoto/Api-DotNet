namespace WebApplication3.Exceptions
{
    public class CepTamanhoInvalidoException : Exception
    {
        public CepTamanhoInvalidoException() : base("O tamanho do cep é de 8 caractéres") { }
    }
}
