using System;
using System.Threading.Tasks;

namespace Prototipo_01.Dominio.Chamados
{
    public interface IChamadosRepositorio
    {
        Task<Chamado> Recuperar(Guid id);
    }
}
