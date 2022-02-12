using Chamados._3.Query.Chamados;
using Microsoft.AspNetCore.Mvc;
using Prototipo_01._1.Aplicacao.Atribuicoes;
using Prototipo_01.Aplicacao.Atribuicoes;
using System;
using System.Threading.Tasks;

namespace Prototipo_01.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class ChamadosController : ControllerBase
    {
        [HttpPut("{idChamado}/Tecnico/{idTecnico}")]
        public async Task<IActionResult> AtribuirChamadoATecnico(
            [FromRoute] Guid idChamado, 
            [FromRoute] Guid idTecnico,
            [FromServices] IAtribuirChamadoATecnicoComandoHandler handler)
        {
            if (await handler.Executar(new AtribuirChamadoATecnicoComando(idTecnico, idChamado)) is var resultado && !resultado.Sucesso)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Valor);
        }

        [HttpDelete("{idChamado}/Tecnico/{idTecnico}")]
        public async Task<IActionResult> DesassociarChamadoDoTecnico(
            [FromRoute] Guid idChamado,
            [FromRoute] Guid idTecnico,
            [FromServices] IDesassociarCasoDoAgenteComandoHandler handler)
        {
            await handler.Executar(new DesassociarChamadoDoTecnicoComando(idTecnico, idChamado));
            return Ok();
        }

        [HttpGet("Abertos")]
        public async Task<IActionResult> RecuperarAbertos([FromServices] IChamadosDataAccess chamadosDataAccess)
        {
            if (await chamadosDataAccess.RecuperarPorStatus("Aberto") is var resultado && !resultado.Sucesso)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Valor);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperarTodos([FromServices] IChamadosDataAccess chamadosDataAccess)
        {
            if (await chamadosDataAccess.Recuperar() is var resultado && !resultado.Sucesso)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Valor);
        }
    }
}
