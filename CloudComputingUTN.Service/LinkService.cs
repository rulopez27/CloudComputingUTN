using CloudComputingUTN.DataAccessLayer;

namespace CloudComputingUTN.Service

{
    public sealed class LinkService : ILinkService
    {
        private readonly LinkGenerator _linkGenerator ;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LinkService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
        }

        public Link Generate(string endpointName, string controller, object? routeValues, string rel, string method)
        {
            var context = _httpContextAccessor.HttpContext ?? new DefaultHttpContext();
            string uri = _linkGenerator.GetUriByAction(context, endpointName, controller, routeValues) ?? $"api/{controller}/{endpointName}";
            return new Link(uri, rel, method);
        }
    }
}
