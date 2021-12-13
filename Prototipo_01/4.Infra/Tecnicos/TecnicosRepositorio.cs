using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_01.Infra.Tecnicos
{
    public sealed class TecnicosRepositorio : ITecnicosRepositorio
    {
        private readonly string _stringConexao;
        private readonly ILogger<TecnicosRepositorio> _logger;

        public TecnicosRepositorio(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _logger = loggerFactory.CreateLogger<TecnicosRepositorio>();
        }

        public async Task<Tecnico> Recuperar(Guid id)
        {
            const string sql = @"SELECT	Tecnicos.Id, 
		                                Tecnicos.Nome
                                 FROM [dbo].[Tecnicos]
                                 WHERE Tecnicos.Id = @id";

            using var conexao = new SqlConnection(_stringConexao);

            try
            {
                await conexao.OpenAsync();
                var resultado = await conexao.QueryFirstOrDefaultAsync(sql, new { id });
                if (resultado is null)
                    throw new Exception($"Nenhum técnico encontrado com o id {id}. String de conexão: {_stringConexao}.");
                return new Tecnico(resultado.Id, resultado.Codinome);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar técnico de id {idTecnico}. String de conexão: {stringConexao}", id, _stringConexao);
                throw;
            } finally
            {
                await conexao.CloseAsync();
            }
        }
    }
}
