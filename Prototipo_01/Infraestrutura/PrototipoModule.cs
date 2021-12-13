using Autofac;
using Prototipo_01._3.Query.Tecnicos;
using Prototipo_01._4.Infra.Tecnicos;
using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using Prototipo_01.Dominio.ServicosDeDominio;
using Prototipo_01.Infra.Tecnicos;
using Prototipo_01.Infra.Atribuicoes;
using Prototipo_01.Infra.Chamados;
using Prototipo_01.Query;

namespace Prototipo_01.Infraestrutura
{
    public sealed class PrototipoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TecnicosRepositorio>().As<ITecnicosRepositorio>();
            builder.RegisterType<TecnicosDataAccess>().As<ITecnicosDataAccess>();
            builder.RegisterType<AtribuicoesDataAccess>().As<IAtribuicoesDataAccess>();
            builder.RegisterType<AtribuicoesRepositorio>().As<IAtribuicoesRepositorio>();
            builder.RegisterType<ChamadosRepositorio>().As<IChamadosRepositorio>();
            builder.RegisterType<EncerrarAtribuicaoAtivaDoChamadoSeHouver>().As<IEncerrarAtribuicaoAtivaDoChamadoSeHouver>();
            base.Load(builder);
        }
    }
}
