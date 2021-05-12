namespace Infra.Localizer
{
    public interface ILocalizer
    {
        public string this[string key] { get; set; }
    }
}
