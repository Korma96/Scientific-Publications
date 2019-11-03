using Microsoft.AspNetCore.Mvc;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Service.Publication;
using System.Threading.Tasks;

namespace ScientificPublications.Publication
{
    public class PublicationController : AbstractController
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            _publicationService = publicationService;
        }

        [HttpGet("send-accepted-mail/{recipient}")]
        public async Task<IActionResult> SendAcceptedMailAsync([FromRoute] string recipient)
        {
            await _publicationService.AcceptPublicationAsync(recipient);
            return Ok();
        }

        [Consumes(Constants.XmlContentType)]
        [HttpPost("send-denied-mail")]
        public async Task<IActionResult> SendDeniedMailAsync([FromBody] DenyPublicationDto denyPublicationDto)
        {
            await _publicationService.DenyPublicationAsync(denyPublicationDto.AuthorEmail, denyPublicationDto.Reason);
            return Ok();
        }
    }
}
