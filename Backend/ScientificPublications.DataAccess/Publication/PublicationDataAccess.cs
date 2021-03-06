﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ScientificPublications.Common;
using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using ScientificPublications.Common.Extensions;
using ScientificPublications.Common.Helpers;
using ScientificPublications.Common.Settings;
using ScientificPublications.DataAccess.Model;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ScientificPublications.DataAccess.Publication
{
    public class PublicationDataAccess : AbstractDataAccess, IPublicationDataAccess
    {
        public PublicationDataAccess(IOptions<AppSettings> appSettings, ILogger<AbstractDataAccess> logger) : base(appSettings, logger, Constants.JavaController.Publication)
        {
        }
        // TODO: prevent user to access forbidden publications
        public async Task<Publications> FindByAuthor(string authorUsername)
        {
            try
            {
                var path = BaseUrl.UrlCombine(authorUsername);
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<publication> FindByIdAsync(string id)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"/id/{id}");
                return await HttpHelper.Get<publication>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> FindByUsernameAndSearchQueryAsync(string username, string searchQuery)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"my-publications-text-search/{username}/{searchQuery}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> FindBySearchQueryAsync(string searchQuery)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"/text-search/{searchQuery}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> FindByReviewerAsync(string reviewerUsername)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"/reviewer-assigned-publications/{reviewerUsername}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> FindByStatusAsync(string status)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"status/{status}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task InsertAsync(publication publication)
        {
            try
            {
                var path = BaseUrl.UrlCombine("insert");
                await HttpHelper.Post(path, publication);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task UpdateStatusAsync(string publicationId, PublicationStatus status)
        {
            try
            {
                var path = BaseUrl.UrlCombine(publicationId);
                await HttpHelper.Put(path, new StatusDto { Status = status.ToString().ToLower() });
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<MemoryStream> GetPublicationAsPdfAsync(string publicationId)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"getByIdPDF/{publicationId}");
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(path);
                    result.EnsureSuccessStatusCode();
                    var memory = new MemoryStream();
                    var stream = await result.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    return memory;
                }
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<MemoryStream> GetPublicationAsHtmlAsync(string publicationId)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"getByIdHtml/{publicationId}");
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(path);
                    result.EnsureSuccessStatusCode();
                    var memory = new MemoryStream();
                    var stream = await result.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(memory);
                    memory.Position = 0;
                    return memory;
                }
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> GetReferencingPublicationsAsync(string publicationId)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"/referencing/{publicationId}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }

        public async Task<Publications> FindByMetadataSearchQueryAsync(string username, string searchQuery)
        {
            try
            {
                var path = BaseUrl.UrlCombine($"/advanced-search/{searchQuery}/{username}");
                return await HttpHelper.Get<Publications>(path);
            }
            catch (Exception e)
            {
                throw new ProxyException(Constants.ExceptionMessages.DatabaseException, e);
            }
        }
    }
}
