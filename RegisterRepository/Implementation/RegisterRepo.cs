using Microsoft.EntityFrameworkCore;
using RegisterDataAccess;
using RegisterDataAccess.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace RegisterRepository.Implementation
{
    public class RegisterRepo : IRegisterRepo
    {
        private readonly AppDBContext _context;

        public RegisterRepo(AppDBContext context) => _context = context;

        private bool HasAny(string name, string password) => _context.Register.AsNoTracking().Any(register => register.Name == name && register.Password == password);
        private void Insert(string name, string password) => _context.Register.Add(new Register() { Name = name, Password = password});
        private async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        bool IRegisterRepo.HasAny(string name, string password) => HasAny(name, password);
        void IRegisterRepo.Insert(string name, string password) => Insert(name, password);
        Task<int> IRegisterRepo.SaveChangesAsync() => SaveChangesAsync();
    }
}
