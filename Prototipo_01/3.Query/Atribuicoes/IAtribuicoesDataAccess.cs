using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototipo_01.Query
{
    public interface IAtribuicoesDataAccess
    {
        Task<IEnumerable<AtribuicoesDoTecnicoViewModel>> RecuperarAtivas(Guid idTecnico);
    }
}
