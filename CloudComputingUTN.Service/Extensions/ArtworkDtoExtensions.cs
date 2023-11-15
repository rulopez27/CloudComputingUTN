using CloudComputingUTN.DataAccessLayer;

namespace CloudComputingUTN.Service.Extensions
{
    public static class ArtworkDtoExtensions
    {
        public static void CreateArtworkLinks(this ArtworkDto artworkDto, ILinkService linkService, LinkGenerator linkGenerator, IHttpContextAccessor context)
        {
            artworkDto.Links.Add(linkService.Generate("Get", "Artworks", new { id = artworkDto.ArtworkId }, "self", "GET"));
            artworkDto.Links.Add(linkService.Generate("Post", "Artworks", null, "create", "POST"));
            artworkDto.Links.Add(linkService.Generate("Put", "Artworks", new { id = artworkDto.ArtworkId }, "update", "PUT"));
            artworkDto.Links.Add(linkService.Generate("Delete", "Artworks", new { id = artworkDto.ArtworkId }, "delete", "DELETE"));
        }
    }
}
