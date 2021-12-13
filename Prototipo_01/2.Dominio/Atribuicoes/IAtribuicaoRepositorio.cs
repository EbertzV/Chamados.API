using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototipo_01.Dominio.Atribuicoes
{
    public interface IAtribuicoesRepositorio
    {
        Task<IEnumerable<Atribuicao>> RecuperarParaTecnico(Guid idTecnico);
        Task<IEnumerable<Atribuicao>> RecuperarParaChamado(Guid idChamado);
        Task<Atribuicao> GravarAtribuicao(Atribuicao atribuicao);
        Task<Atribuicao> AlterarAtribuicao(Atribuicao atribuicao);
    }
}
