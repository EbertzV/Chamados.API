using System;

namespace Prototipo_01.Aplicacao.Atribuicoes
{
    public sealed class AtribuirChamadoATecnicoComando
    {
        public AtribuirChamadoATecnicoComando(Guid idTecnico, Guid idChamado)
        {
            IdTecnico = idTecnico;
            IdChamado = idChamado;
        }

        public Guid IdTecnico { get; }
        public Guid IdChamado { get; }
    }
}
