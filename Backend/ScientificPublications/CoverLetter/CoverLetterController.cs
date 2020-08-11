using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using ScientificPublications.Common.Utility;
using ScientificPublications.Service.CoverLetter;

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
        public async Task<IActionResult> FindByAuthor([FromRoute] string publicationId)
        {
            if (string.IsNullOrWhiteSpace(publicationId))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var coverLetter = await _coverLetterService.FindByPublicationIdAsync(publicationId);

            return Ok(ToXml(coverLetter));
        }
    }
}