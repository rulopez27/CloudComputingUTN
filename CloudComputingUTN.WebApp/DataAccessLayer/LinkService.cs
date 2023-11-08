using Microsoft.AspNetCore.Routing;
using System.Runtime.CompilerServices;

namespace CloudComputingUTN.WebApp.DataAccessLayer
{
    internal sealed class LinkService : ILinkService
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public Link Generate(string endpointName, string? controller, object? routeValues, string rel, string method)
        {
            return new Link(_linkGenerator.GetUriByAction(_httpContextAccessor.HttpContext, endpointName, controller, routeValues), rel, method);
        }
    }
}
