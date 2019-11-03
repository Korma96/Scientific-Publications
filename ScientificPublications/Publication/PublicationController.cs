using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
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

        private readonly AppSettings _appSettings;

        public PublicationController(IPublicationService publicationService, IOptions<AppSettings> appSettings)
        {
            _publicationService = publicationService;
            _appSettings = appSettings.Value;
        }

        [HttpGet("xsd-schema")]
        public async Task<IActionResult> GetXsdSchemaFileAsync()
        {
            var file = await _publicationService.GetXsdSchemaAsync();
            return File(file, Constants.XmlContentType, _appSettings.Paths.PublicationXsdSchema);
        }

        [HttpPost("upload")]
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
