using CloudComputingUTN.WebApp.DataAccessLayer;

namespace CloudComputingUTN.WebApp.Extensions
{
    public static class ArtworkDtoExtensions
    {
        public static void CreateArtworkLinks(this ArtworkDto artworkDto, LinkGenerator linkGenerator, IHttpContextAccessor context)
        {
            LinkService linkService = new LinkService(linkGenerator, context);
            artworkDto.Links.Add(linkService.Generate("Get", "Artworks", new { id = artworkDto.ArtworkId }, "self", "GET"));
            artworkDto.Links.Add(linkService.Generate("Post", "Artworks", null, "create", "POST"));
            artworkDto.Links.Add(linkService.Generate("Put", "Artworks", new { id = artworkDto.ArtworkId }, "update", "PUT"));
            artworkDto.Links.Add(linkService.Generate("Delete", "Artworks", new { id = artworkDto.ArtworkId }, "delete", "DELETE"));
        }
    }
}
