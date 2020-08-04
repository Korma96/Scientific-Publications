using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Service.Publication;
using System;
using System.Linq;
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            var fileContent = file.OpenReadStream().StreamToString();
            _publicationService.ValidatePublicationFile(fileContent);
            await _publicationService.InsertAsync(fileContent);

            return Ok();
        }

        [HttpGet("my")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindByAuthor()
        {
            var publications = await _publicationService.FindByAuthorAsync(GetSession().Username);
            return Ok(ToXml(publications));
        }

        [HttpGet("status/{status}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindByStatus([FromRoute] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var publications = await _publicationService.FindByStatusAsync(status);
            return Ok(ToXml(publications));
        }

        [HttpGet("statuses")]
        [AuthorizationFilter(Role.Author)]
        public IActionResult GetAllStatuses()
        {
            var names = Enum.GetNames(typeof(PublicationStatus));
            var lowerNames = names.Select(x => x.ToLower()).ToList();
            return Ok(lowerNames);
        }
    }
}
