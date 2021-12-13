using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prototipo_01._3.Query.Tecnicos;
using Prototipo_01._4.Infra.Tecnicos;
using Prototipo_01.Aplicacao.Atribuicoes;
using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using Prototipo_01.Dominio.ServicosDeDominio;
using Prototipo_01.Infra.Tecnicos;
using Prototipo_01.Infra.Atribuicoes;
using Prototipo_01.Infra.Chamados;
using Prototipo_01.Infraestrutura;
using Prototipo_01.Query;
using Chamados._3.Query.Chamados;
using Chamados._4.Infra.Chamados;

namespace Prototipo_01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAtribuicoesDataAccess, AtribuicoesDataAccess>();
            services.AddScoped<IEncerrarAtribuicaoAtivaDoChamadoSeHouver, EncerrarAtribuicaoAtivaDoChamadoSeHouver>();
            services.AddScoped<IAtribuirChamadoATecnicoComandoHandler, AtribuirChamadoATecnicoComandoHandler>();
            services.AddScoped<IAtribuicoesRepositorio, AtribuicoesRepositorio>();
            services.AddScoped<ITecnicosRepositorio, TecnicosRepositorio>();
            services.AddScoped<IChamadosRepositorio, ChamadosRepositorio>();
            services.AddScoped<ITecnicosDataAccess, TecnicosDataAccess>();
            services.AddScoped<IChamadosDataAccess, ChamadosDataAccess>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule(new PrototipoModule() { });

            var container = containerBuilder.Build();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
