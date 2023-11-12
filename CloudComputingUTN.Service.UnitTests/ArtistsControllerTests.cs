using AutoMapper;
using CloudComputingUTN.DataAccessLayer;
using CloudComputingUTN.DataAccessLayer.Profiles;
using CloudComputingUTN.Entities;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.Service.Controllers.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace CloudComputingUTN.Service.UnitTests
{
    [TestFixture]
    public class ArtistsControllerTests
    {
        Mock<IMuseumDbRepository> _mockRepository;
        IMapper _mapper;
        Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        Mock<LinkGenerator> _mockLinkGenerator;

        [SetUp]
        public void Setup()
        {
            //Mock IMuseumDbRepository
            _mockRepository = new Mock<IMuseumDbRepository>();
            _mockRepository.Setup(m => m.GetArtists()).ReturnsAsync(ArtistsCollection());

            //Mock IMapper
            var mappingConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ArtistProfile());
                mc.AddProfile(new ArtworkProfile());
            });

            _mapper = mappingConfiguration.CreateMapper();

            //Mock IHttpContextAccesor
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.Setup(ca => ca.HttpContext).Returns(new DefaultHttpContext());
            
            _mockLinkGenerator = new Mock<LinkGenerator>();
        }

        [Test]
        public async Task GetArtists_WhenCalled_ReturnsOk() 
        {
            ArtistsController controller = new ArtistsController(_mockRepository.Object, _mapper, _mockHttpContextAccessor.Object);
            var actionResult = await controller.Get(_mockLinkGenerator.Object);
            Assert.IsNotNull(actionResult);
            Assert.That(actionResult, Is.TypeOf(typeof(OkObjectResult)));
        }

        public ICollection<Artist> ArtistsCollection()
        {
            List<Artist> artists = new List<Artist>();
            artists.Add(new Artist()
            {
                ArtistName = "Leonardo Da Vinci",
                ArtistWikiPage = "https://artsandculture.google.com/entity/leonardo-da-vinci/m04lg6?categoryId=artist",
                ArtworkGallery = new List<Artwork>()
                            {
                                new Artwork
                                {
                                    ArtworkName = "La Gioconda",
                                    ArtworkYear = 1519,
                                    ArtworkDescription = "This portrait is believed to be of Lisa Gherardini, the wife of Florentine fabric merchant Francesco del Giocondo. In Italian it is known as \"La Gioconda\", but in English it is commonly referred to as \"The Mona Lisa.\"",
                                    ArtworkURL = "https://artsandculture.google.com/asset/portrait-of-lisa-gherardini-wife-of-francesco-del-giocondo-known-as-monna-lisa-la-gioconda-or-mona-lisa-1503-1519-leonardo-di-ser-piero-da-vinci-dit-l%C3%A9onard-de-vinci-1452-1519-paris-mus%C3%A9e-du-louvre/uQGZ28lYUJ3OGw"
                                }
                            }
            });
            artists.Add(new Artist()
            {
                ArtistName = "Vincent Van Gogh",
                ArtistWikiPage = "https://artsandculture.google.com/project/van-gogh",
                ArtworkGallery = new List<Artwork>()
                            {
                            new Artwork
                            {
                                ArtworkName = "The Starry Night",
                                ArtworkYear = 1889,
                                ArtworkDescription = "Van Gogh’s night sky is a field of roiling energy. Below the exploding stars, the village is a place of quiet order.",
                                ArtworkURL = "https://artsandculture.google.com/asset/the-starry-night-vincent-van-gogh/bgEuwDxel93-Pg"
                            },
                            new Artwork
                            {
                                ArtworkName = "The Sunflowers",
                                ArtworkYear = 1888,
                                ArtworkDescription = "Van Gogh’s paintings of Sunflowers are among his most famous. He did them in Arles, in the south of France, in 1888 and 1889.",
                                ArtworkURL = "https://artsandculture.google.com/asset/sunflowers-vincent-van-gogh/hwEGmsM-FoHAwA"
                            }
                            }
            });
            return artists.ToList();
        }
    }
}