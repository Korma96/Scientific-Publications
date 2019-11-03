using System.Threading.Tasks;

namespace ScientificPublications.Service.Email
{
    public interface IEmailService : ISingletonService
    {
        Task SendEmailAsync(string subject, string body, string[] to);
    }
}
