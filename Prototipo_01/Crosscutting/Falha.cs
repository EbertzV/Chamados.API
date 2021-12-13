namespace Prototipo_01.Crosscutting
{
    public sealed class Falha
    {
        private Falha(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; }

        public static Falha NovaFalha(string mensagem)
            => new Falha(mensagem);
    }
}
