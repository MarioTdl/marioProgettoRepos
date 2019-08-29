using System.Threading.Tasks;

namespace marioProgetto.Persistence
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}