using AutoMapper;
using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.DataAccessLayer.Profiles
{
    public class ArtworkProfile : Profile
    {
        public ArtworkProfile()
        {
            CreateMap<Artwork, ArtworkDto>()
                .ForMember(
                    dest => dest.ArtistId,
                    opt => opt.MapFrom(src => src.ArtistId)
                )
                .ForMember(
                    dest => dest.ArtworkId,
                    opt => opt.MapFrom(src => src.ArtworkId)
                )
                .ForMember(
                    dest => dest.ArtworkURL,
                    opt => opt.MapFrom(src => src.ArtworkURL)
                )
                .ForMember(
                    dest => dest.ArtworkYear,
                    opt => opt.MapFrom(src => src.ArtworkYear ?? 0)
                )
                .ForMember(
                    dest => dest.ArtworkName,
                    opt => opt.MapFrom(src => src.ArtworkName)
                )
                .ForMember(
                    dest => dest.ArtworkDescription,
                    opt => opt.MapFrom(src => src.ArtworkDescription)
                );
        }
    }
}
