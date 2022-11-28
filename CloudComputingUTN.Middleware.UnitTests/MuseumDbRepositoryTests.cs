using CloudComputingUTN.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CloudComputingUTN.Middleware.UnitTests
{
    [TestFixture]
    public class MuseumDbRepositoryTests
    {
        DbConnection _connection;
        DbContextOptions<MuseumDbContext> _options;
        MuseumDbContext CreateContext() => new MuseumDbContext(_options);
        IMuseumDbRepository MuseumDbRepository;
        Artist newArtist;
        Artwork newArtwork;
        Artist existingArtist;
        Artwork existingArtwork;
        public void Dispose() => _connection.Dispose();

        [SetUp]
        public void Setup()
        {

            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<MuseumDbContext>().UseSqlite(_connection).Options;

            using (var context = new MuseumDbContext(_options))
            {
                if (context.Database.EnsureCreated())
                {
                    context.AddRange(
                    new Artist
                    {
                        ArtistName = "Leonardo Da Vinci",
                        ArtistWikiPage = "https://mockurl.com/DaVinci",
                        ArtworkGallery = new List<Artwork>()
                            {
                            new Artwork{ ArtworkName = "La Gioconda",  ArtworkYear = 1519, ArtworkDescription = "Da Vinci's most famous paint"}
                            }
                    },
                    new Artist
                    {
                        ArtistName = "Vincent Van Gogh",
                        ArtistWikiPage = "https://mockurl.com/VanGogh",
                        ArtworkGallery = new List<Artwork>()
                            {
                            new Artwork{ ArtworkName = "The Starry Night",  ArtworkYear = 1889, ArtworkDescription = "Van Gogh's most famous paint"},
                            new Artwork{ ArtworkName = "The Sunflowers",  ArtworkYear = 1888, ArtworkDescription = "Van Gogh's first paintings"}
                            }
                    }
                    );
                    context.SaveChanges();

                    existingArtist = context.Artists.FirstOrDefault();
                    existingArtwork = context.Artworks.FirstOrDefault();
                }
            }
            
            MuseumDbRepository = new MuseumDbRepository(CreateContext());

            newArtist = new Artist
            {
                ArtistName = "Pablo Picasso",
                ArtistWikiPage = "http://randourl.com/Picasso",
                ArtworkGallery = new List<Artwork>()
                {
                    new Artwork
                    {
                        ArtworkName = "Guernica",
                        ArtworkDescription = "Painted between may and june 1937 makes reference to Guernica bombing during civil war",
                        ArtworkYear = 1937,
                        ArtworkURL = "http://randomurl/Guernica"
                    }
                }
            };

            newArtwork = new Artwork
            {
                ArtworkName = "Lorem Ipsum",
                ArtworkDescription = "Lorem ipsum dolor sit amet",
                ArtworkYear = DateTime.Now.Year,
                ArtworkURL = "http://loremipsum.lorem",
                ArtistId = 1
            };

            
        }

        [Test]
        public async Task GetArtists_WhenCalled_ReturnsCollectionOfArtists()
        {
            var artists = await MuseumDbRepository.GetArtists();
            Assert.IsNotEmpty(artists);
        }

        [Test]
        public async Task GetArtworks_WhenCalled_ReturnsCollectionOfArtworks()
        {
            var artworks = await MuseumDbRepository.GetArtworks();
            Assert.IsNotEmpty(artworks);
        }

        [Test]
        public async Task CreateArtist_WhenCalled_ReturnsCreatedArtist()
        {
            var artist = await MuseumDbRepository.CreateArtist(newArtist);
            Assert.That(artist.ArtistId, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task CreateArtwork_WhenCalled_ReturnsCreatedArtwork()
        {
            var artwork = await MuseumDbRepository.CreateArtwork(newArtwork);
            Assert.That(artwork.ArtworkId, Is.Not.EqualTo(0));
        }

        [Test]
        public async Task GetArtistById_ArtistExists_ReturnsFoundArtist()
        {
            var artist = await MuseumDbRepository.GetArtistById(1);
            Assert.That(artist, Is.Not.Null);
            Assert.That(artist.ArtistName, Is.EqualTo("Leonardo Da Vinci"));
        }

        [Test]
        public void GetArtistById_ArtistDoesNotExists_ThrowsInvalidOperationException()
        {
            Exception exception =
            Assert.ThrowsAsync<InvalidOperationException>(async () => await MuseumDbRepository.GetArtistById(99));
            Assert.That(exception.Message, Is.EqualTo("Sequence contains no elements."));
        }

        [Test]
        public async Task GetArtworkById_ArtworkExists_ReturnsFoundArtwork()
        {
            var artwork = await MuseumDbRepository.GetArtworkById(1);
            Assert.That(artwork, Is.Not.Null);
            Assert.That(artwork.ArtworkName, Is.EqualTo("La Gioconda"));
        }

        [Test]
        public void GetArtworkById_ArtworkDoesNotExists_ThrowsInvalidOperationException()
        {
            Exception exception =
            Assert.ThrowsAsync<InvalidOperationException>(async () => await MuseumDbRepository.GetArtworkById(99));
            Assert.That(exception.Message, Is.EqualTo("Sequence contains no elements."));
        }

        [Test]
        public async Task UpdateArtist_NameChanged_ReturnsUpdatedArtist()
        {
            existingArtist.ArtistName = "Test name";
            Artist artist = await MuseumDbRepository.UpdateArtist(existingArtist);
            Assert.That(artist.ArtistName, Is.EqualTo("Test name"));
        }

        [Test]
        public async Task UpdateArtist_WikiURLChanged_ReturnsUpdatedArtist()
        {
            existingArtist.ArtistWikiPage = "https://wikiurl/";
            Artist artist = await MuseumDbRepository.UpdateArtist(existingArtist);
            Assert.That(artist.ArtistWikiPage, Is.EqualTo("https://wikiurl/"));
        }

        [Test]
        public async Task UpdateArtwork_NameChanged_ReturnsUpdatedArtwork()
        {
            existingArtwork.ArtworkName = "Test name";
            Artwork artwork = await MuseumDbRepository.UpdateArtwork(existingArtwork);
            Assert.That(artwork.ArtworkName, Is.EqualTo("Test name"));
        }

        [Test]
        public async Task UpdateArtwork_ArtworkURLChanged_ReturnsUpdatedArtwork()
        {
            existingArtwork.ArtworkURL = "https://test/";
            Artwork artwork = await MuseumDbRepository.UpdateArtwork(existingArtwork);
            Assert.That(artwork.ArtworkURL, Is.EqualTo("https://test/"));
        }
    }
}