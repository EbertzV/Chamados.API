using Prototipo_01.Crosscutting;
using System.Threading.Tasks;

namespace Prototipo_01._3.Query.Tecnicos
{
    public interface ITecnicosDataAccess
    {
        Task<Resultado<TecnicosDisponiveisViewModel>> RecuperarDisponiveisAsync();
    }
}
