using Register.DataAccess.Entities;

namespace Register.Repository
{
    public interface IAuthenticationRepo
    {
        void Insert(Authentication authentication);
        Authentication First(string refreshToken);
        int SaveChanges();
    }
}
