using System;
using System.Threading.Tasks;

namespace Prototipo_01.Dominio.Tecnicos
{
    public interface ITecnicosRepositorio
    {
        Task<Tecnico> Recuperar(Guid id);
    }
}
