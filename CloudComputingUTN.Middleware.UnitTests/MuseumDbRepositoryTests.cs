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
        DbContextOptions _options;
        SQLiteDbContext CreateContext() => new SQLiteDbContext(_options);
        IMuseumDbRepository MuseumDbRepository;
        public void Dispose() => _connection.Dispose();
        
        [SetUp]
        public void Setup()
        {

            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _options = new DbContextOptionsBuilder<BaseDbContext>().UseSqlite(_connection).Options;
            
            using var context = new SQLiteDbContext(_options);
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
            }
            
            MuseumDbRepository = new MuseumDbRepository(CreateContext());
        }

        [Test]
        public async Task GetArtists_WhenCalled_ReturnsCollectionOfArtists()
        {
            var artists = await MuseumDbRepository.GetArtists();
            Assert.IsNotEmpty(artists);
        }
    }
}