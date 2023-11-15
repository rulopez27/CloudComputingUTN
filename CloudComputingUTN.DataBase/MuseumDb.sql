USE master
GO

CREATE DATABASE MuseumDb
GO

USE MuseumDb

PRINT N'Creating Table [dbo].[Artists]...';


GO
CREATE TABLE [dbo].[Artists] (
    [ArtistId]       INT           IDENTITY (1, 1) NOT NULL,
    [ArtistName]     VARCHAR (100) NOT NULL,
    [ArtistWikiPage] VARCHAR (300) NULL,
    PRIMARY KEY CLUSTERED ([ArtistId] ASC)
);


GO
PRINT N'Creating Table [dbo].[Artworks]...';


GO
CREATE TABLE [dbo].[Artworks] (
    [ArtworkId]          INT           IDENTITY (1, 1) NOT NULL,
    [ArtistId]           INT           NOT NULL,
    [ArtworkName]        VARCHAR (100) NOT NULL,
    [ArtworkYear]        INT           NULL,
    [ArtworkDescription] VARCHAR (300) NULL,
    [ArtworkURL]         VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ArtworkId] ASC)
);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Artworks]...';


GO
ALTER TABLE [dbo].[Artworks]
    ADD FOREIGN KEY ([ArtistId]) REFERENCES [dbo].[Artists] ([ArtistId]);


GO

DECLARE @ArtistID INT = 0

IF NOT EXISTS (SELECT * FROM dbo.Artists WHERE ArtistName = 'Leonardo Da Vinci')
    BEGIN
        INSERT INTO dbo.Artists(ArtistName, ArtistWikiPage) VALUES
        ('Leonardo Da Vinci', N'https://artsandculture.google.com/entity/leonardo-da-vinci/m04lg6?categoryId=artist')

        SELECT @ArtistID = SCOPE_IDENTITY()

        INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
        (@ArtistID, 'La Gioconda', 1519, N'This portrait is believed to be of Lisa Gherardini, the wife of Florentine fabric merchant Francesco del Giocondo. In Italian it is known as \"La Gioconda\", but in English it is commonly referred to as \"The Mona Lisa.\"', 
        N'https://artsandculture.google.com/asset/portrait-of-lisa-gherardini-wife-of-francesco-del-giocondo-known-as-monna-lisa-la-gioconda-or-mona-lisa-1503-1519-leonardo-di-ser-piero-da-vinci-dit-l%C3%A9onard-de-vinci-1452-1519-paris-mus%C3%A9e-du-louvre/uQGZ28lYUJ3OGw')
    END

SET @ArtistID = 0

IF NOT EXISTS (SELECT * FROM dbo.Artists WHERE ArtistName = 'Vincent Van Gogh')
    BEGIN
        INSERT INTO dbo.Artists(ArtistName, ArtistWikiPage) VALUES
        ('Vincent Van Gogh', N'https://artsandculture.google.com/project/van-gogh')

        SELECT @ArtistID = SCOPE_IDENTITY()

        INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
        (@ArtistID, 'The Starry Night', 1889, N'Van Gogh’s night sky is a field of roiling energy. Below the exploding stars, the village is a place of quiet order.', 
        N'https://artsandculture.google.com/asset/the-starry-night-vincent-van-gogh/bgEuwDxel93-Pg'),
        (@ArtistID, 'Sunflowers', 1889, N'Van Gogh’s paintings of Sunflowers are among his most famous. He did them in Arles, in the south of France, in 1888 and 1889.',
        N'https://artsandculture.google.com/asset/sunflowers-vincent-van-gogh/hwEGmsM-FoHAwA')
    END

SET @ArtistID = 0

IF NOT EXISTS (SELECT * FROM dbo.Artists WHERE ArtistName = 'Claude Monet')
    BEGIN
        INSERT INTO dbo.Artists(ArtistName, ArtistWikiPage) VALUES
        ('Claude Monet', N'https://artsandculture.google.com/project/monetwashere')

        SELECT @ArtistID = SCOPE_IDENTITY()

        INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
        (@ArtistID, 'The Thames at Westminster', 1871, N'En otoño de 1870, París estaba asediada durante la guerra franco-prusiana, y Monet huyó de Francia con su joven familia. Se instalaron en Londres, donde pintó esta vista brumosa del Támesis la primavera siguiente.', N'https://artsandculture.google.com/story/VwKyDFXzPLGdIg')
    END

SET @ArtistID = 0

IF NOT EXISTS (SELECT * FROM dbo.Artists WHERE ArtistName = 'Pablo Picasso')
    BEGIN
        INSERT INTO dbo.Artists(ArtistName, ArtistWikiPage) VALUES
        ('Pablo Picasso', N'https://artsandculture.google.com/entity/pablo-picasso/m060_7?categoryId=artist')

        SELECT @ArtistID = SCOPE_IDENTITY()

        INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
        (@ArtistID, 'Toros', 1960, N'Pintura hecha por Picasso en 1960', 
        N'https://artsandculture.google.com/asset/toros-illustration-1960-pablo-ruiz-picasso/OQHCYysg78-c5w')
    END

GO

PRINT N'Update complete.';


GO
