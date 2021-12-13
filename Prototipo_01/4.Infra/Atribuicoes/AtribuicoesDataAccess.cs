using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_01.Infra.Atribuicoes
{
    public sealed class AtribuicoesDataAccess : IAtribuicoesDataAccess
    {
		private readonly string _stringConexao;
		private readonly ILogger<AtribuicoesDataAccess> _logger;

		public AtribuicoesDataAccess(IConfiguration configuration, ILoggerFactory loggerFactory)
		{
			_stringConexao = configuration.GetConnectionString("DefaultConnection");
			_logger = loggerFactory.CreateLogger<AtribuicoesDataAccess>();
		}
		public async Task<IEnumerable<AtribuicoesDoTecnicoViewModel>> RecuperarAtivas(Guid idTecnico)
        {
			const string sql = @"WITH Tecnico AS (
									SELECT	Id,
											Nome
									FROM Tecnicos
									WHERE Id = @idTecnico
								), Atribuicao AS (
									SELECT	Tecnico.Id AS IdTecnico,
											Tecnico.Nome AS NomeTecnico,
											Atribuicoes.IdCaso,
											Atribuicoes.DataInicio,
											Atribuicoes.DataFim,
											Atribuicoes.Ativa
									FROM Tecnicos
									LEFT JOIN Atribuicoes 
										ON Atribuicoes.IdTecnico = Tecnico.Id
								) SELECT Atribuicao.*,
											Chamados.Descricao,
											Chamados.Status
								FROM Atribuicao
								INNER JOIN Chamados 
									ON Chamados.Id = Atribuicao.IdChamado";

			using var conexao = new SqlConnection(_stringConexao);
			try
			{
				await conexao.OpenAsync();

				var resultado = await conexao.QueryAsync(sql, new { idTecnico });

				return resultado.GroupBy(
					k => new { k.IdTecnico, k.NomeTecnico },
					g => new { g.IdChamado, g.DataInicio, g.DataFim, g.Descricao, g.Status },
					(chave, valor) => new AtribuicoesDoTecnicoViewModel(
						chave.IdTecnico, 
						chave.NomeTecnico, 
						valor.Select(v => new ChamadoDoTecnicoViewModel(v.IdChamado, v.Descricao))));
			} catch (Exception ex)
			{
				throw;
			} finally
			{
				await conexao.CloseAsync();
			}

		}
    }
}
