using System.Threading.Tasks;

namespace Notifier.Business
{
    public interface IBusinessManager
    {
        Task SendMailAsync(string header, string subject, string body, params string[] toList);
    }
}
