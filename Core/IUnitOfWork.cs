using System.Threading.Tasks;

namespace marioProgetto.Core
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}