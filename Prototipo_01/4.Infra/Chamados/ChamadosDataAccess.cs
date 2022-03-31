using Chamados._3.Query.Chamados;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01.Crosscutting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Chamados._4.Infra.Chamados
{
    public sealed class ChamadosDataAccess : IChamadosDataAccess
    {
        private readonly string _stringConexao;
        private readonly ILogger<ChamadosDataAccess> _logger;

        public ChamadosDataAccess(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _logger = loggerFactory.CreateLogger<ChamadosDataAccess>();
        }

        public async Task<Resultado<IEnumerable<ChamadoViewModel>>> Recuperar()
        {
            const string sql = @"SELECT	 Chamados.Id,
		                                 Chamados.Descricao,
		                                 Chamados.DataCriacao,
		                                 Tecnicos.Id AS IdTecnico,
		                                 Tecnicos.Nome AS NomeTecnico
                                 FROM Chamados
                                 LEFT JOIN Atribuicoes
	                                 ON Atribuicoes.IdChamado = Chamados.Id
	                                 AND Atribuicoes.Ativa = 1
                                 LEFT JOIN Tecnicos
	                                 On Tecnicos.Id = Atribuicoes.IdTecnico";
            using var conexao = new SqlConnection(_stringConexao);
            try
            {
                await conexao.OpenAsync();
                var resultado = await conexao.QueryAsync(sql);
                return Resultado<IEnumerable<ChamadoViewModel>>
                    .NovoSucesso(resultado.Select(r => new ChamadoViewModel(r.Id.ToString().ToUpper(), r.Descricao, r.DataCriacao, r.IdTecnico, r.NomeTecnico)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar chamados. String de conexão: {stringConexao}.", _stringConexao);
                throw;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }

        public async Task<Resultado<IEnumerable<ChamadoAbertoViewModel>>> RecuperarPorStatus(string status)
        {
            const string sql = @"SELECT	Id,
		                                Descricao,
		                                DataCriacao
                                 FROM Chamados
                                 WHERE Status = @status";
            using var conexao = new SqlConnection(_stringConexao);
            try
            {
                await conexao.OpenAsync();
                var resultado = await conexao.QueryAsync(sql, new { status });
                return Resultado<IEnumerable<ChamadoAbertoViewModel>>
                    .NovoSucesso(resultado.Select(r => new ChamadoAbertoViewModel(r.Id, r.Descricao, r.DataCriacao)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao recuperar chamados com o status {status}. String de conexão: {stringConexao}.", status, _stringConexao);
                throw;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }
    }
}
