using Infra.Resource.DataAccess;
using Infra.Resource.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infra.Resource.Repository.Implementation
{
    public class LocalizationRepo : ILocalizationRepo
    {
        private readonly LocalizerDBContext _context;

        public LocalizationRepo(LocalizerDBContext context) => _context = context;

        private string First(string language, string key)
            => _context.Localizations.AsNoTracking().FirstOrDefault(x => x.Language == language && x.Key == key)?.Value;

        private void InsertOrUpdate(string language, string key, string value)
        {
            var existLocalization = _context.Localizations.FirstOrDefault(x => x.Language == language && x.Key == key);
            if (existLocalization == null)
                _context.Localizations.Add(new Localization() { Language = language, Key = key, Value = value });
            else
                existLocalization.Value = value;
        }

        private int SaveChanges() => _context.SaveChanges();

        string ILocalizationRepo.First(string language, string key) => First(language, key);
        void ILocalizationRepo.InsertOrUpdate(string language, string key, string value) => InsertOrUpdate(language, key, value);
        int ILocalizationRepo.SaveChanges() => SaveChanges();
    }
}
