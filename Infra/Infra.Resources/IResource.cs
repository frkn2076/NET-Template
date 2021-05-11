namespace Infra.Resources
{
    public interface IResource
    {
        public string this[string key] { get; set; }
    }
}
