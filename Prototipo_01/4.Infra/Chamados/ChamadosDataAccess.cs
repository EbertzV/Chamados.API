﻿using Chamados._3.Query.Chamados;
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
