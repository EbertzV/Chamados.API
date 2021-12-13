using Prototipo_01._1.Aplicacao.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using Prototipo_01.Dominio.ServicosDeDominio;
using System.Threading.Tasks;

namespace Prototipo_01.Aplicacao.Atribuicoes
{
    public interface IDesassociarCasoDoAgenteComandoHandler
    {
        Task Executar(DesassociarChamadoDoTecnicoComando comando);
    }

    public sealed class DesassociarCasoDoTecnicoComandoHandler : IDesassociarCasoDoAgenteComandoHandler
    {        
        private readonly IChamadosRepositorio _chamadosRepositorio;
        private readonly IEncerrarAtribuicaoAtivaDoChamadoSeHouver _encerrarAtribuicaoAtivaDoChamadoSeHouver;

        public DesassociarCasoDoTecnicoComandoHandler(IChamadosRepositorio chamadosRepositorio, IEncerrarAtribuicaoAtivaDoChamadoSeHouver encerrarAtribuicaoAtivaDoChamado)
        {
            _chamadosRepositorio = chamadosRepositorio;
            _encerrarAtribuicaoAtivaDoChamadoSeHouver = encerrarAtribuicaoAtivaDoChamado;
        }

        public async Task Executar(DesassociarChamadoDoTecnicoComando comando)
        {
            var chamado = await _chamadosRepositorio.Recuperar(comando.ChamadoId);
            await _encerrarAtribuicaoAtivaDoChamadoSeHouver.Encerrar(chamado);
            return;
        }
    }
}
