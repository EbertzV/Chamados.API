using System;

namespace Prototipo_01.Crosscutting
{
    public sealed class Resultado<T> where T : class
    {
        private Resultado(bool sucesso, T valor, Falha falha)
        {
            Sucesso = sucesso;
            Valor = valor;
            Falha = falha;
        }

        public bool Sucesso { get; }
        public T Valor { get; }
        public Falha Falha { get; }

        public static Resultado<T> NovoSucesso(T valor)
            => new Resultado<T>(true, valor, null);

        public static Resultado<T> NovaFalha(Falha falha)
            => new Resultado<T>(false, null, falha);
    }
}
