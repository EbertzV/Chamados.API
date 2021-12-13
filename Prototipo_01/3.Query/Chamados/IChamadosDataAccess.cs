using Prototipo_01.Crosscutting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chamados._3.Query.Chamados
{
    public interface IChamadosDataAccess
    {
        Task<Resultado<IEnumerable<ChamadoAbertoViewModel>>> RecuperarPorStatus(string status);
    }
}
