using Prototipo_01.Crosscutting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chamados._3.Query.Chamados
{
    public interface IChamadosDataAccess
    {
        Task<Resultado<IEnumerable<ChamadoAbertoViewModel>>> RecuperarPorStatus(string status);
        Task<Resultado<IEnumerable<ChamadoViewModel>>> Recuperar();
        Task<Resultado<ChamadoViewModel>> Recuperar(Guid id);
    }
}
