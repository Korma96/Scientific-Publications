using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Service.Publication;
using System.Threading.Tasks;

namespace ScientificPublications.Publication
{
    public class PublicationController : AbstractController
    {
        private readonly IPublicationService _publicationService;

        public PublicationController(IOptions<AppSettings> appSettings, IPublicationService publicationService) : base(appSettings)
        {
            _publicationService = publicationService;
        }

        [HttpGet("xsd-schema")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetXsdSchemaFileAsync()
        {
            var file = await _publicationService.GetXsdSchemaAsync();
            return File(file, Constants.XmlContentType, AppSettings.Paths.PublicationXsdSchema);
        }

        [HttpPost("upload")]
        [AuthorizationFilter(Role.Author)]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            try
            {
                var fileContent = file.OpenReadStream().StreamToString();
                _publicationService.ValidatePublicationFile(fileContent);
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("send-accepted-mail/{recipient}")]
        [AuthorizationFilter(Role.JournalEditor)]
        public async Task<IActionResult> SendAcceptedMailAsync([FromRoute] string recipient)
        {
            await _publicationService.AcceptPublicationAsync(recipient);
            return Ok();
        }

        [Consumes(Constants.XmlContentType)]
        [AuthorizationFilter(Role.JournalEditor)]
        [HttpPost("send-denied-mail")]
        public async Task<IActionResult> SendDeniedMailAsync([FromBody] DenyPublicationDto denyPublicationDto)
        {
            await _publicationService.DenyPublicationAsync(denyPublicationDto.AuthorEmail, denyPublicationDto.Reason);
            return Ok();
        }
    }
}
