using Microsoft.EntityFrameworkCore;
using Register.DataAccess;
using Register.DataAccess.Entities;
using System.Linq;

namespace Register.Repository.Implementation
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly AppDBContext _context;

        public AuthenticationRepo(AppDBContext context) => _context = context;

        private void Insert(Authentication authentication) => _context.Authentications.Add(authentication);
        private Authentication First(string refreshToken) => _context.Authentications.AsNoTracking().FirstOrDefault(x => x.RefreshToken == refreshToken);
        private int SaveChanges() => _context.SaveChanges();

        Authentication IAuthenticationRepo.First(string refreshToken) => First(refreshToken);
        void IAuthenticationRepo.Insert(Authentication authentication) => Insert(authentication);
        int IAuthenticationRepo.SaveChanges() => SaveChanges();
    }
}
