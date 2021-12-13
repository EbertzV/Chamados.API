using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01._3.Query.Tecnicos;
using Prototipo_01.Crosscutting;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_01._4.Infra.Tecnicos
{
    public sealed class TecnicosDataAccess : ITecnicosDataAccess
    {
        private readonly string _stringConexao;
        private readonly ILogger<TecnicosDataAccess> _logger;

        public TecnicosDataAccess(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _logger = loggerFactory.CreateLogger<TecnicosDataAccess>();
        }

        public async Task<Resultado<TecnicosDisponiveisViewModel>> RecuperarDisponiveisAsync()
        {
            const string sql = @"SELECT Id, 
                                        Nome
                                 FROM Tecnicos (NOLOCK)";

            using var conexao = new SqlConnection(_stringConexao);
            try
            {
                await conexao.OpenAsync();
                var resultado = await conexao.QueryAsync(sql);

                return Resultado<TecnicosDisponiveisViewModel>.NovoSucesso(new TecnicosDisponiveisViewModel(
                    resultado
                        .Select(
                            r => new TecnicoDisponivelViewModel(
                                r.Id, 
                                r.Nome))));
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar tecnicos disponíveis.");
                throw;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }
}
