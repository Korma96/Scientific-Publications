using AutoMapper;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificPublications.Publication
{
    public class PublicationController : AbstractController
    {
        private readonly IPublicationService _publicationService;

        private readonly IMapper _mapper;

        public PublicationController(
            IOptions<AppSettings> appSettings, 
            IPublicationService publicationService,
            IMapper mapper) : base(appSettings)
        {
            _publicationService = publicationService;
            _mapper = mapper;
        }

        [HttpGet("xsd-schema")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetXsdSchemaFileAsync()
        {
            var file = await _publicationService.GetXsdSchemaAsync();
            return File(file, Constants.XmlContentType, AppSettings.Paths.PublicationXsd);
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
        public async Task<IActionResult> FindByAuthor([FromQuery] bool shortForm)
        {
            var publications = await _publicationService.FindByAuthorAsync(GetSession().Username);
            if (shortForm)
            {
                var publicationDtos = _mapper.Map<List<PublicationDto>>(publications.PublicationList);
                return Ok(publicationDtos);
            }
            return Ok(ToXml(publications));
        }

        [HttpGet("status/{status}")]
        [AuthorizationFilter(Role.Editor)]
        public async Task<IActionResult> FindByStatus([FromRoute] string status, [FromQuery] bool shortForm)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var publications = await _publicationService.FindByStatusAsync(status);
            if (shortForm)
            {
                var publicationDtos = _mapper.Map<List<PublicationDto>>(publications.PublicationList);
                return Ok(publicationDtos);
            }
            return Ok(ToXml(publications));
        }

        [HttpPut("{nextStatus}/{publicationId}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> UpdateStatus([FromRoute] string publicationId, [FromRoute] string nextStatus)
        {
            if (string.IsNullOrWhiteSpace(publicationId))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            if (string.IsNullOrWhiteSpace(nextStatus))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            // TODO: 1. reviewer case: verify in workflow if reviewer is assigned for that publication
            //       2. author case: verify if author owns publication with given id
            //       3. add mail notifications
            await _publicationService.UpdateStatusWithValidationAsync(publicationId, nextStatus, GetSession().Role);

            return Ok();
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
