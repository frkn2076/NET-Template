namespace Infra.Resource.Repository
{
    public interface ILocalizationRepo
    {
        public string First(string language, string key);
        public void InsertOrUpdate(string language, string key, string value);
        public int SaveChanges();
    }
}
