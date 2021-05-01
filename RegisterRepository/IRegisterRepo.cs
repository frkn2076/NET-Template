using System.Threading.Tasks;

namespace RegisterRepository
{
    public interface IRegisterRepo
    {
        bool HasAny(string name, string password);
        void Insert(string name, string password);
        Task<int> SaveChangesAsync();
    }
}