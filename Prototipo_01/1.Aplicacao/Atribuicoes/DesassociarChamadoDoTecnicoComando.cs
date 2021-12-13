using System;

namespace Prototipo_01._1.Aplicacao.Atribuicoes
{
    public sealed class DesassociarChamadoDoTecnicoComando
    {
        public DesassociarChamadoDoTecnicoComando(Guid tecnicoId, Guid chamadoId)
        {
            TecnicoId = tecnicoId;
            ChamadoId = chamadoId;
        }

        public Guid TecnicoId { get; }
        public Guid ChamadoId { get; }
    }
}
