using Infra.Localizer;
using Infra.Resource.Repository;

namespace Infra.Resource.Implementation
{
    public class Localizer : ILocalizer
    {
        private readonly ILocalizationRepo _localizationRepo;
        private const string _language = "TR";
        public Localizer(ILocalizationRepo localizationRepo)
        {
            _localizationRepo = localizationRepo;
        }

        public string this[string key]
        {
            get => _localizationRepo.First(_language, key);
            set 
            {
                _localizationRepo.InsertOrUpdate(_language, key, value);
                _localizationRepo.SaveChanges();
            }
        }
    }
}
