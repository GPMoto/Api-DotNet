namespace WebApplication3.Exceptions
{
    public class ContatoStatusInvalidoException : Exception
    {
        public ContatoStatusInvalidoException() : base("O valor de status é somente 0 e 1") { }
    }
}
