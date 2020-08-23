using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Service.CoverLetter;
using System.Threading.Tasks;

namespace ScientificPublications.CoverLetter
{
    public class CoverLetterController : AbstractController
    {
        private readonly ICoverLetterService _coverLetterService;

        public CoverLetterController(IOptions<AppSettings> appSettings, ICoverLetterService coverLetterService) : base(appSettings)
        {
            _coverLetterService = coverLetterService;
        }

        [HttpGet("xsd-schema")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetXsdSchemaFileAsync()
        {
            var file = await _coverLetterService.GetXsdSchemaAsync();
            return File(file, Constants.XmlContentType, AppSettings.Paths.CoverLetterXsd);
        }

        [HttpPost("upload")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            var fileContent = file.OpenReadStream().StreamToString();
            _coverLetterService.ValidateCoverLetterFile(fileContent);
            await _coverLetterService.InsertAsync(fileContent);

            return Ok();
        }

        [HttpGet("{publicationId}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindByPublication([FromRoute] string publicationId)
        {
            if (string.IsNullOrWhiteSpace(publicationId))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var coverLetter = await _coverLetterService.FindByPublicationIdAsync(publicationId);
            return Ok(ToXml(coverLetter));
        }
    }
}