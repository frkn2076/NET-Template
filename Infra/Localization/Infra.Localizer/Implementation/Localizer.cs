using Infra.Localizer;
using Infra.Resource.Repository;
using System;

namespace Infra.Resource.Implementation
{
    public class Localizer : ILocalizer
    {
        private readonly ILocalizationRepo _localizationRepo;
        private readonly string _language;
        public Localizer(ILocalizationRepo localizationRepo)
        {
            _localizationRepo = localizationRepo;
            _language = Environment.GetEnvironmentVariable("Language");
        }

        public string this[string key]
        {
            get 
            { 
                return _localizationRepo.First(_language, key);
            }
            set 
            {
                _localizationRepo.InsertOrUpdate(_language, key, value);
                _localizationRepo.SaveChanges();
            }
        }
    }
}
