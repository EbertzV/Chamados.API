using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using System.Threading.Tasks;

namespace Prototipo_01.Dominio.ServicosDeDominio
{
    public interface IEncerrarAtribuicaoAtivaDoChamadoSeHouver
    {
        Task Encerrar(Chamado chamado);
    }

    public sealed class EncerrarAtribuicaoAtivaDoChamadoSeHouver : IEncerrarAtribuicaoAtivaDoChamadoSeHouver
    {
        private readonly IAtribuicoesRepositorio _atribuicoesRepositorio;

        public EncerrarAtribuicaoAtivaDoChamadoSeHouver(IAtribuicoesRepositorio atribuicoesRepositorio)
        {
            _atribuicoesRepositorio = atribuicoesRepositorio;
        }

        public async Task Encerrar(Chamado chamado)
        {
            var atribuicoes = await _atribuicoesRepositorio.RecuperarParaChamado(chamado.Id);
            if (atribuicoes.RecuperarAtivaParaChamadoSeHouver(chamado) is var ativa && ativa == null)
                return;
            ativa.Encerrar();
            await _atribuicoesRepositorio.AlterarAtribuicao(ativa);
        }
    }
}
