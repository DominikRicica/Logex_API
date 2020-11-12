using Microsoft.AspNetCore.WebUtilities;
using Presentation.Queries;
using Presentation.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }
        public Uri GetEstablishmentUri(int establishmentId, GetDetailQuery query = null)
        {
            return new Uri(_baseUri + ApiRoutes.Establishment.Get.Replace("{Id}", establishmentId.ToString()));
        }

        public Uri GetAllEstablishmentsUri(PaginationQuery pagination = null)
        {
            var uri = _baseUri + ApiRoutes.Establishment.GetAll;

            if (pagination != null)
            {
                uri = QueryHelpers.AddQueryString(uri, "pageNumber", pagination.PageNumber.ToString());
                uri = QueryHelpers.AddQueryString(uri, "pageSize", pagination.PageSize.ToString());
            }
            return new Uri(uri);
        }
    }
}
