using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prototipo_01.Dominio.Tecnicos;
using Prototipo_01.Dominio.Atribuicoes;
using Prototipo_01.Dominio.Chamados;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Prototipo_01.Infra.Atribuicoes
{
    public sealed class AtribuicoesRepositorio : IAtribuicoesRepositorio
    {
        private readonly string _stringConexao;
        private readonly ILogger<AtribuicoesRepositorio> _logger;

        public AtribuicoesRepositorio(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _stringConexao = configuration.GetConnectionString("DefaultConnection");
            _logger = loggerFactory.CreateLogger<AtribuicoesRepositorio>();
        }

        public async Task<IEnumerable<Atribuicao>> RecuperarParaTecnico(Guid idTecnico)
        {
            const string sql = @"WITH Atrib AS (
								 	 SELECT  IdTecnico, 
								 	 		 IdChamado, 
								 	 		 DataInicio,
								 	 		 DataFim, 
								 	 		 Ativa
								 	 FROM Atribuicoes
								 	 WHERE IdTecnico = @idTecnico
								 ), Tecnico AS (
								 	 SELECT	 Tecnicos.Id AS IdTecnico,
								 	 		 Tecnicos.Nome AS NomeTecnico,
								 	 		 Atrib.DataInicio AS InicioAtribuicao,
								 	 		 Atrib.DataFim AS FimAtribuicao,
								 	 		 Atrib.Ativa AS AtivaAtribuicao,
								 	 		 Atrib.IdChamado AS AtribChamado
								 	 FROM Tecnicos
								 	 INNER JOIN Atrib 
								 		 ON Atrib.IdTecnico = Tecnicos.Id
								 ) SELECT	Tecnico.IdTecnico,
								 			Tecnico.NomeTecnico,
								 			Tecnico.InicioAtribuicao,
								 			Tecnico.FimAtribuicao,
								 			Tecnico.AtivaAtribuicao,
								 			Chamados.Id AS IdChamado,
								 			Chamados.Descricao AS DescricaoChamado,
								 			Chamados.Status AS StatusChamado
								 FROM Tecnico
								 INNER JOIN Chamados
									ON Agt.AtribChamado = Chamados.Id";

            using var conexao = new SqlConnection(_stringConexao);
            try
            {
                await conexao.OpenAsync();

                var resultado = await conexao.QueryAsync(sql, new { idTecnico });
                return resultado.Select(r => new Atribuicao(
                    new Tecnico(r.IdTecnico, r.NomeTecnico),
                    new Chamado(r.IdChamado, Enum.Parse(typeof(EStatusChamado), r.StatusChamado),r.DescricaoChamado),
                    r.InicioAtribuicao,
                    r.FimAtribuicao, r.AtivaAtribuicao));
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await conexao.CloseAsync();
            }
        }

        public async Task<IEnumerable<Atribuicao>> RecuperarParaChamado(Guid idChamado)
        {
			const string sql = @"  WITH Chamado AS (
										SELECT	Id,
												Descricao,
												Status
										FROM Chamados
										WHERE Id = @idChamado
									), Atribuicao AS (
										SELECT  Chamado.Id AS ChamadoId,
												Chamado.Descricao AS ChamadoDescricao,
												Chamado.Status AS ChamadoStatus,
												Atribuicoes.IdTecnico AS AtribuicaoIdTecnico,
												Atribuicoes.DataInicio AS AtribuicaoDataInicio,
												Atribuicoes.DataFim AS AtribuicaoDataFim,
												Atribuicoes.Ativa AS AtribuicaoAtiva
										FROM Chamado
										INNER JOIN Atribuicoes
											ON Atribuicoes.IdChamado = Chamado.Id
									) SELECT	Atribuicao.*,
												Tecnicos.Id AS IdTecnico,
												Tecnicos.Nome AS NomeTecnico
									FROM Atribuicao
									INNER JOIN Tecnicos
										ON Tecnicos.Id = Atribuicao.AtribuicaoIdTecnico";

			using var conexao = new SqlConnection(_stringConexao);

			try
			{
				await conexao.OpenAsync();
				var resultado = await conexao.QueryAsync(sql, new { idChamado });
				return resultado.Select(r => new Atribuicao(
					new Tecnico(r.IdAgente, r.CodinomeAgente), 
					new Chamado(r.CasoId, Enum.Parse(typeof(EStatusChamado), r.CasoStatus), r.CasoDescricao),
					r.AtribuicaoDataInicio,
					r.AtribuicaoDataFim,
					r.AtribuicaoAtiva));
			} catch (Exception ex)
			{
				throw;
			}
			finally
			{
				await conexao.CloseAsync();
			}
		}

		public async Task<Atribuicao> GravarAtribuicao(Atribuicao atribuicao)
		{
			const string sql = @"INSERT INTO Atribuicoes (IdTecnico, IdChamado, DataInicio, DataFim, Ativa)
								 VALUES (@idTecnico, @idChamado, @dataInicio, @dataFim, @ativa)";

			using var conexao = new SqlConnection(_stringConexao);

			try
			{
				await conexao.OpenAsync();
				var resultado = await conexao.ExecuteAsync(sql, new
				{
					idTecnico = atribuicao.Tecnico.Id,
					idChamado = atribuicao.Chamado.Id,
					dataInicio = atribuicao.DataInicial,
					dataFim = atribuicao.DataFinal,
					ativa = atribuicao.Ativa
				});

				if (resultado <= 0)
					throw new Exception("Falha ao incluir atribuição.");
				return atribuicao;
			} catch (Exception ex)
			{
				throw;
			}
			finally
			{
				await conexao.CloseAsync();
			}
		}

		public async Task<Atribuicao> AlterarAtribuicao(Atribuicao atribuicao)
		{
			const string sql = @"UPDATE Atribuicoes 
								 SET	Ativa = @ativa, 
										DataFim  = @dataFim
							     WHERE IdTecnico = @idTecnico
									 AND IdChamado = @idChamado";

			using var conexao = new SqlConnection(_stringConexao);

			try
			{
				await conexao.OpenAsync();
				var resultado = await conexao.ExecuteAsync(sql, new
				{
					idTecnico = atribuicao.Tecnico.Id,
					idChamado = atribuicao.Chamado.Id,
					dataFim = atribuicao.DataFinal,
					ativa = atribuicao.Ativa
				});

				if (resultado <= 0)
					throw new Exception("Falha ao atualizar atribuição.");
				return atribuicao;
			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				await conexao.CloseAsync();
			}
		}
	}
}
