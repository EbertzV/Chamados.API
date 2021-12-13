using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01.Dominio.Chamados;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Prototipo_01.Infra.Chamados
{
    public sealed class ChamadosRepositorio : IChamadosRepositorio
    {
        private readonly string _stringConexao;
        private readonly ILogger<ChamadosRepositorio> _logger;

        public ChamadosRepositorio(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _logger = loggerFactory.CreateLogger<ChamadosRepositorio>();
        }

        public async Task<Chamado> Recuperar(Guid id)
        {
            const string sql = @"SELECT	Id,
		                                Descricao,
		                                Status
                                 FROM [dbo].[Chamados]
                                 WHERE Id = @id";

            using var conexao = new SqlConnection(_stringConexao);

            try
            {
                await conexao.OpenAsync();
                var resultado = await conexao.QueryFirstOrDefaultAsync(sql, new { id });
                if (resultado == null)
                    throw new Exception($"Não foi encontrado um chamado com o id {id}.");
                return new Chamado(resultado.Id, Enum.Parse(typeof(EStatusChamado), resultado.Status), resultado.Descricao);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar chamado de id {idChamado}. String de conexão: {stringConexao}", id, _stringConexao);
                throw;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }
}
