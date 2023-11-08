using CloudComputingUTN.WebApp.DataAccessLayer;

namespace CloudComputingUTN.WebApp.Extensions
{
    public static class ArtistDtoExtensions
    {
        public static void CreateArtistLinks(this ArtistDto artistDto, LinkGenerator linkGenerator, IHttpContextAccessor context)
        {
            LinkService linkService = new LinkService(linkGenerator, context);
            artistDto.Links.Add(linkService.Generate("Get", "Artists", new {id = artistDto.ArtistId}, "self", "GET"));
            artistDto.Links.Add(linkService.Generate("Post", "Artists", null, "create", "POST"));
            artistDto.Links.Add(linkService.Generate("Put", "Artists", null, "update", "PUT"));
        }
    }
}
