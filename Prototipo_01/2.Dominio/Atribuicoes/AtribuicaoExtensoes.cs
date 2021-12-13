using Prototipo_01.Dominio.Chamados;
using System.Collections.Generic;
using System.Linq;

namespace Prototipo_01.Dominio.Atribuicoes
{
    public static class AtribuicaoExtensoes
    {
        public static Atribuicao RecuperarAtivaParaChamadoSeHouver(this IEnumerable<Atribuicao> atribuicoes, Chamado chamado)
            => atribuicoes.FirstOrDefault(a => a.EstaAtivaParaChamado(chamado));
    }
}
