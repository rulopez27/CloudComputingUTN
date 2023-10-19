using AutoMapper;
using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.DataAccessLayer.Profiles
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistDto>()
                .ForMember(
                    dest => dest.ArtistId,
                    opt => opt.MapFrom(src => src.ArtistId)
                )
                .ForMember(
                    dest => dest.ArtistName,
                    opt => opt.MapFrom(src => src.ArtistName)
                )
                .ForMember(
                    dest => dest.ArtistWikiPage,
                    opt => opt.MapFrom(src => src.ArtistWikiPage)
                )
                .ForMember(
                    dest => dest.Artworks,
                    opt => opt.MapFrom(src => src.ArtworkGallery)
                );
        }
    }
}
