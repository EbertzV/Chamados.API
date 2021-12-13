using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Chamados;
using System;

namespace Prototipo_01.Dominio.Atribuicoes
{
    public sealed class Atribuicao
    {
        public Atribuicao(Tecnico tecnico, Chamado chamado, DateTime dataInicial, DateTime? dataFinal, bool ativa)
        {
            Tecnico = tecnico;
            Chamado = chamado;
            DataInicial = dataInicial;
            DataFinal = dataFinal;
            Ativa = ativa;
        }

        public Tecnico Tecnico { get; }
        public Chamado Chamado { get; }
        public DateTime DataInicial { get; }
        public DateTime? DataFinal { get; private set; }
        public bool Ativa { get; private set; }

        public bool EstaAtivaParaChamado(Chamado chamado)
            => Ativa 
            && Chamado.Equals(chamado) 
            && DataAtualEhValida();

        public bool DataAtualEhValida()
            => DataInicial <= DateTime.Now 
            && (DataFinal == null || DataFinal >= DateTime.Now);

        public static Atribuicao Nova(Tecnico tecnico, Chamado chamado)
            => new Atribuicao(tecnico, chamado, DateTime.Now, null, true);

        public void Encerrar()
        {
            Ativa = false;
            DataFinal = DateTime.Now;
        }
    }
}
