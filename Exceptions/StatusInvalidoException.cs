namespace WebApplication3.Exceptions
{
    public class StatusInvalidoException : Exception
    {
        public StatusInvalidoException() : base("O valor de status é somente 0 e 1") { }
    }
}
