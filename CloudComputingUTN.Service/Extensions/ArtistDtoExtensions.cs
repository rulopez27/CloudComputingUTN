using CloudComputingUTN.DataAccessLayer;

namespace CloudComputingUTN.Service.Extensions
{
    public static class ArtistDtoExtensions
    {
        public static void CreateArtistLinks(this ArtistDto artistDto, ILinkService linkService, LinkGenerator linkGenerator, IHttpContextAccessor context)
        {
            artistDto.Links.Add(linkService.Generate("Get", "Artists", new {id = artistDto.ArtistId}, "self", "GET"));
            artistDto.Links.Add(linkService.Generate("Post", "Artists", null, "create", "POST"));
            artistDto.Links.Add(linkService.Generate("Put", "Artists", null, "update", "PUT"));
            artistDto.Links.Add(linkService.Generate("Delete", "Artists", null, "delete", "DELETE"));

            artistDto.Artworks.ForEach(artwork => artwork.CreateArtworkLinks(linkGenerator, context));
        }
    }
}
