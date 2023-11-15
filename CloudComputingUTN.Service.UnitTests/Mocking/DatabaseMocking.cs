using CloudComputingUTN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudComputingUTN.Service.UnitTests.Mocking
{
    static class DatabaseMocking
    {
        public static ICollection<Artist> ArtistsCollection()
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

        public static Artist GetArtistById(int id)
        {
            Artist artist;
            if (id == 1)
            {
                artist = new Artist()
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
                };
            }
            else
            {
                artist = null;
            }
            return artist;
        }

        public static Artist GetNewArtist()
        {
            return new Artist()
            {
                ArtistName = "Lorem Ipsusm",
                ArtistWikiPage = "https://loremipsum.com",
                ArtworkGallery = new List<Artwork>()
                            {
                                new Artwork
                                {
                                    ArtworkName = "Lorem Ipsum dolor sit amet",
                                    ArtworkYear = 1519,
                                    ArtworkDescription = "KLorem ipsum dolor sit amet",
                                    ArtworkURL = "https://loremipsum.com"
                                }
                            }
            };
        }

        public static ICollection<Artwork> ArtworksCollection()
        {
            List<Artwork> artworks = new List<Artwork>();
            artworks.Add(new Artwork
            {
                ArtworkName = "La Gioconda",
                ArtworkYear = 1519,
                ArtworkDescription = "This portrait is believed to be of Lisa Gherardini, the wife of Florentine fabric merchant Francesco del Giocondo. In Italian it is known as \"La Gioconda\", but in English it is commonly referred to as \"The Mona Lisa.\"",
                ArtworkURL = "https://artsandculture.google.com/asset/portrait-of-lisa-gherardini-wife-of-francesco-del-giocondo-known-as-monna-lisa-la-gioconda-or-mona-lisa-1503-1519-leonardo-di-ser-piero-da-vinci-dit-l%C3%A9onard-de-vinci-1452-1519-paris-mus%C3%A9e-du-louvre/uQGZ28lYUJ3OGw"
            });
            artworks.Add(new Artwork
            {
                ArtworkName = "The Starry Night",
                ArtworkYear = 1889,
                ArtworkDescription = "Van Gogh’s night sky is a field of roiling energy. Below the exploding stars, the village is a place of quiet order.",
                ArtworkURL = "https://artsandculture.google.com/asset/the-starry-night-vincent-van-gogh/bgEuwDxel93-Pg"
            });
            artworks.Add(new Artwork
            {
                ArtworkName = "The Sunflowers",
                ArtworkYear = 1888,
                ArtworkDescription = "Van Gogh’s paintings of Sunflowers are among his most famous. He did them in Arles, in the south of France, in 1888 and 1889.",
                ArtworkURL = "https://artsandculture.google.com/asset/sunflowers-vincent-van-gogh/hwEGmsM-FoHAwA"
            });
            return artworks.ToList();
        }
        public static Artwork GetArtwork(int id)
        {
            Artwork artwork;
            if (id == 1)
            {
                artwork = new Artwork
                {
                    ArtworkName = "La Gioconda",
                    ArtworkYear = 1519,
                    ArtworkDescription = "This portrait is believed to be of Lisa Gherardini, the wife of Florentine fabric merchant Francesco del Giocondo. In Italian it is known as \"La Gioconda\", but in English it is commonly referred to as \"The Mona Lisa.\"",
                    ArtworkURL = "https://artsandculture.google.com/asset/portrait-of-lisa-gherardini-wife-of-francesco-del-giocondo-known-as-monna-lisa-la-gioconda-or-mona-lisa-1503-1519-leonardo-di-ser-piero-da-vinci-dit-l%C3%A9onard-de-vinci-1452-1519-paris-mus%C3%A9e-du-louvre/uQGZ28lYUJ3OGw"
                };
            }
            else
            {
                artwork = null;
            }

            return artwork;
        }

        public static Artwork GetNewArtwork()
        {
            return new Artwork()
            {
                ArtworkName = "Lorem Ipsum",
                ArtworkYear = 1519,
                ArtworkDescription = "Lorem ipsum dolor sit amet",
                ArtworkURL = "https://lorem.com"
            };
        }
    }
}
