using System.Threading.Tasks;

namespace ScientificPublications.Service.Email
{
    public interface IEmailService : ISingletonService
    {
        Task SendEmailAsync(EmailEntity emailEntity);

        Task SendEmailAsync(string subject, string body, string[] to);
    }
}
