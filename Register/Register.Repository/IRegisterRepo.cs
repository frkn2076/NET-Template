using System.Threading.Tasks;

namespace Register.Repository
{
    public interface IRegisterRepo
    {
        bool HasAny(string name, string password);
        void Insert(string name, string password);
        Task<int> SaveChangesAsync();
    }
}