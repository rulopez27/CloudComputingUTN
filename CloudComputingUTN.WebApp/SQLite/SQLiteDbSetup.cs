using Microsoft.Data.Sqlite;
using System.Data.Common;
using CloudComputingUTN.Middleware;
using Microsoft.EntityFrameworkCore;
using CloudComputingUTN.Entities;

namespace CloudComputingUTN.WebApp.SQLite
{
    public class SQLiteDbSetup
    {
        private DbConnection _connection;
        private DbContextOptions<MuseumDbContext> _options;
        public SQLiteDbSetup()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _options = new DbContextOptionsBuilder<MuseumDbContext>().UseSqlite(_connection).Options;
        }

        public DbConnection GetDbConnection()
        {
            return _connection;
        }

        public DbContextOptions<MuseumDbContext> GetDbContextOptions()
        {
            return _options;
        }

        public void SetupInMemoryDatabase()
        {
            using (var context = new MuseumDbContext(_options))
            {
                if (context.Database.EnsureCreated())
                {
                    context.AddRange(
                    new Artist
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
                    },
                    new Artist
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
                    },
                   new Artist
                   {
                      ArtistName = "Claude Monet",
                      ArtistWikiPage = "https://artsandculture.google.com/project/monetwashere",
                      ArtworkGallery = new List<Artwork>()
                      {
                            new Artwork
                            {
                                ArtworkName = "The Thames at Westminster",
                                ArtworkYear = 1871,
                                ArtworkDescription = "En otoño de 1870, París estaba asediada durante la guerra franco-prusiana, y Monet huyó de Francia con su joven familia. Se instalaron en Londres, donde pintó esta vista brumosa del Támesis la primavera siguiente.",
                                ArtworkURL = "https://artsandculture.google.com/story/VwKyDFXzPLGdIg"
                            }
                      }
                    },
                   new Artist
                   {
                      ArtistName = "Pablo Picasso",
                      ArtistWikiPage = "https://artsandculture.google.com/entity/pablo-picasso/m060_7?categoryId=artist",
                      ArtworkGallery = new List<Artwork>()
                      {
                            new Artwork
                            {
                                ArtworkName = "Toros",
                                ArtworkYear = 1960,
                                ArtworkDescription = "Pintura hecha por Picasso en 1960",
                                ArtworkURL = "https://artsandculture.google.com/asset/toros-illustration-1960-pablo-ruiz-picasso/OQHCYysg78-c5w"
                            }
                      }
                    }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
