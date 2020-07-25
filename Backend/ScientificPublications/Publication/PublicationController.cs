using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            var fileContent = file.OpenReadStream().StreamToString();
            _publicationService.ValidatePublicationFile(fileContent);
            await _publicationService.InsertAsync(fileContent);

            return Ok();
        }

        [HttpGet("{author}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindByAuthor([FromRoute] string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var publications = await _publicationService.FindByAuthor(author);

            return Ok(ToXml(publications));
        }
    }
}
