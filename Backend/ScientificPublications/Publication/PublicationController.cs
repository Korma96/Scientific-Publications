using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ScientificPublications.Application;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
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

        /// <summary>
        /// Get xsd schema for publications
        /// </summary>
        [HttpGet("xsd-schema")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetXsdSchemaFileAsync()
        {
            var file = await _publicationService.GetXsdSchemaAsync();
            return File(file, Constants.XmlContentType, AppSettings.Paths.PublicationXsd);
        }

        /// <summary>
        /// Author: upload new publication
        /// </summary>
        [HttpPost("upload")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            var fileContent = file.OpenReadStream().StreamToString();
            _publicationService.ValidatePublicationFile(fileContent);

            await _publicationService.InsertAsync(fileContent);
            await _publicationService.SendAuthorUploadPublicationMail(GetSession().Username, fileContent);

            return Ok();
        }

        /// <summary>
        /// Author: upload revision and specify previousPublicationId
        /// </summary>
        [HttpPost("revision/{previousPublicationId}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> UploadRevisionFile(IFormFile file, [FromRoute] string previousPublicationId)
        {
            if (file.Length == 0)
                return BadRequest(Constants.ExceptionMessages.EmptyFile);

            var fileContent = file.OpenReadStream().StreamToString();
            _publicationService.ValidatePublicationFile(fileContent);

            await _publicationService.InsertRevisionAsync(fileContent, previousPublicationId);
            await _publicationService.SendAuthorRevisedPublicationMail(fileContent);
            return Ok();
        }

        /// <summary>
        /// Author: get all my publications
        /// </summary>
        [HttpGet("my")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindByAuthor([FromQuery] bool shortForm)
        {
            var publications = await _publicationService.FindByAuthorAsync(GetSession().Username);
            return PublicationsResponse(publications, shortForm);
        }

        /// <summary>
        /// Search published publications by text
        /// </summary>
        [HttpGet("search/published/{searchQuery}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindBySearchQuery([FromRoute] string searchQuery, [FromQuery] bool shortForm)
        {
            var publications = await _publicationService.FindBySearchQueryAsync(searchQuery);
            return PublicationsResponse(publications, shortForm);
        }

        /// <summary>
        /// Author: search my publications by text
        /// </summary>
        [HttpGet("search/my/{searchQuery}")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> FindMyBySearchQuery([FromRoute] string searchQuery, [FromQuery] bool shortForm)
        {
            var publications = await _publicationService.FindMyBySearchQueryAsync(GetSession().Username, searchQuery);
            return PublicationsResponse(publications, shortForm);
        }

        /// <summary>
        /// Editor: find publications with specified status
        /// </summary>
        [HttpGet("status/{status}")]
        [AuthorizationFilter(Role.Editor)]
        public async Task<IActionResult> FindByStatus([FromRoute] string status, [FromQuery] bool shortForm)
        {
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(Constants.ExceptionMessages.EmptyValue);

            var publications = await _publicationService.FindByStatusAsync(status);
            return PublicationsResponse(publications, shortForm);
        }

        /// <summary>
        /// Update publication status
        /// Available statuses: /api/Publication/statuses
        /// State diagram: https://github.com/Korma96/Scientific-Publications/blob/development/doc/state_diagram.jpg
        /// </summary>
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
            await _publicationService.UpdateStatusWithValidationAndEmailNotificationAsync(publicationId, nextStatus, GetSession().Role, GetSession().Username);

            return Ok();
        }

        /// <summary>
        /// Get all available publication statuses
        /// </summary>
        [HttpGet("statuses")]
        [AuthorizationFilter(Role.Author)]
        public IActionResult GetAllStatuses()
        {
            var names = Enum.GetNames(typeof(PublicationStatus));
            var lowerNames = names.Select(x => x.ToLower()).ToList();
            return Ok(lowerNames);
        }

        /// <summary>
        /// Get assigned publications for currently logged reviewer
        /// </summary>
        [HttpGet("reviewer")]
        [AuthorizationFilter(Role.Author)]
        public async Task<IActionResult> GetReviewerPublications([FromQuery] bool shortForm)
        {
            var publications = await _publicationService.FindByReviewerAsync(GetSession().Username);
            return PublicationsResponse(publications, shortForm);
        }

        /// <summary>
        /// Download publication in pdf format
        /// </summary>
        [HttpGet("download/pdf/{publicationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicationAsPDF([FromRoute] string publicationId)
        {
            var publication = await _publicationService.GetByIdWithValidationAsync(publicationId);
            var publicationPDF = await _publicationService.DownloadPublicationAsPdfAsync(publicationId);
            return File(publicationPDF, Constants.PdfContentType, publication.title);
        }

        /// <summary>
        /// Download publication in html format
        /// </summary>
        [HttpGet("download/html/{publicationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicationAsHTML([FromRoute] string publicationId)
        {
            var publication = await _publicationService.GetByIdWithValidationAsync(publicationId);
            var publicationHtml = await _publicationService.DownloadPublicationAsHtmlAsync(publicationId);
            return File(publicationHtml, Constants.HtmlContentType, publication.title + ".html");
        }

        /// <summary>
        /// Get publications which are referencing given publication
        /// </summary>
        [HttpGet("referencing/{publicationId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReferencingPublications([FromRoute] string publicationId, [FromQuery] bool shortForm)
        {
            var publications = await _publicationService.GetReferencingPublications(publicationId);
            return PublicationsResponse(publications, shortForm);
        }

        private IActionResult PublicationsResponse(Publications publications, bool shortForm)
        {
            if (shortForm)
            {
                var publicationDtos = _mapper.Map<List<PublicationDto>>(publications.PublicationList);
                return Ok(publicationDtos);
            }
            return Ok(ToXml(publications));
        }
    }
}
