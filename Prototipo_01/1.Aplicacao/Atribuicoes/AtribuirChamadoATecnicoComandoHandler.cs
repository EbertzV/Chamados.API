using Prototipo_01.Crosscutting;
using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using Prototipo_01.Dominio.ServicosDeDominio;
using System.Threading.Tasks;

namespace Prototipo_01.Aplicacao.Atribuicoes
{
    public interface IAtribuirChamadoATecnicoComandoHandler
    {
        Task<Resultado<Atribuicao>> Executar(AtribuirChamadoATecnicoComando comando);
    }

    public sealed class AtribuirChamadoATecnicoComandoHandler : IAtribuirChamadoATecnicoComandoHandler
    {
        private readonly ITecnicosRepositorio _tecnicosRepositorio;
        private readonly IChamadosRepositorio _chamadosRepositorio;
        private readonly IAtribuicoesRepositorio _atribuicoesRepositorio;
        private readonly IEncerrarAtribuicaoAtivaDoChamadoSeHouver _encerrarAtribuicaoAtivaDoChamadoSeHouver;

        public AtribuirChamadoATecnicoComandoHandler(ITecnicosRepositorio tecnicosRepositorio, IChamadosRepositorio chamadosRepositorio, IAtribuicoesRepositorio atribuicoesRepositorio, IEncerrarAtribuicaoAtivaDoChamadoSeHouver encerrarAtribuicaoAtivaDoChamado)
        {
            _tecnicosRepositorio = tecnicosRepositorio;
            _chamadosRepositorio = chamadosRepositorio;
            _atribuicoesRepositorio = atribuicoesRepositorio;
            _encerrarAtribuicaoAtivaDoChamadoSeHouver = encerrarAtribuicaoAtivaDoChamado;
        }

        public async Task<Resultado<Atribuicao>> Executar(AtribuirChamadoATecnicoComando comando)
        {
            var tecnico = await _tecnicosRepositorio.Recuperar(comando.IdTecnico);
            var chamado = await _chamadosRepositorio.Recuperar(comando.IdChamado);

            await _encerrarAtribuicaoAtivaDoChamadoSeHouver.Encerrar(chamado);
            
            var novaAtribuicao = Atribuicao.Nova(tecnico, chamado);
            await _atribuicoesRepositorio.GravarAtribuicao(novaAtribuicao);

            return Resultado<Atribuicao>.NovoSucesso(novaAtribuicao);
        }
    }
}
