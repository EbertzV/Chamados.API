using Microsoft.AspNetCore.Mvc;
using Prototipo_01._3.Query.Tecnicos;
using Prototipo_01.Query;
using System;
using System.Threading.Tasks;

namespace Prototipo_01.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class TecnicosController : ControllerBase
    {
        [HttpGet("{idTecnico}/ChamadosAtribuidos")]
        public async Task<IActionResult> RecuperarChamadosDoTecnico([FromRoute] Guid idTecnico, [FromServices] IAtribuicoesDataAccess atribuicoesDataAccess)
            => Ok(await atribuicoesDataAccess.RecuperarAtivas(idTecnico));

        [HttpGet("TecnicosDisponiveis")]
        public async Task<IActionResult> RecuperarTecnicosDisponiveis([FromServices] ITecnicosDataAccess tecnicosDataAccess)
        {
            if (await tecnicosDataAccess.RecuperarDisponiveisAsync() is var resultado && !resultado.Sucesso)
                return BadRequest(resultado.Falha);
            return Ok(resultado.Sucesso);
        }
    }
}
