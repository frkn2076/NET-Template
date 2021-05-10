using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notifier.Business
{
    public interface IBusinessManager
    {
        Task SendMail(string header, string subject, string body, params string[] toList);
    }
}
