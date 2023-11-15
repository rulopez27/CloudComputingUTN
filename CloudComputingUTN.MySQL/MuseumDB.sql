USE MuseumDb;

CREATE TABLE Artists
(
    ArtistId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ArtistName VARCHAR(200),
    ArtistWikiPage VARCHAR(3000)
);

CREATE UNIQUE INDEX IX_ArtistName ON Artists(ArtistName);

CREATE TABLE Artworks
(
    ArtworkId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ArtistId INT NOT NULL,
    ArtworkName VARCHAR(200) NOT NULL,
    ArtworkYear INT,
    ArtworkDescription VARCHAR(300),
    ArtworkURL VARCHAR(3000),
    FOREIGN KEY (ArtistId) REFERENCES Artists(ArtistID) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IX_ArtworkName ON Artworks(ArtistId, ArtworkName);

SELECT @ArtistID;

INSERT INTO Artists(ArtistName, ArtistWikiPage) VALUES
('Leonardo Da Vinci', N'https://artsandculture.google.com/entity/leonardo-da-vinci/m04lg6?categoryId=artist');

SET @ArtistID = @@IDENTITY;

INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
(@ArtistID, 'La Gioconda', 1519, N'This portrait is believed to be of Lisa Gherardini, the wife of Florentine fabric merchant Francesco del Giocondo. In Italian it is known as \"La Gioconda\", but in English it is commonly referred to as \"The Mona Lisa.\"', 
        N'https://artsandculture.google.com/asset/portrait-of-lisa-gherardini-wife-of-francesco-del-giocondo-known-as-monna-lisa-la-gioconda-or-mona-lisa-1503-1519-leonardo-di-ser-piero-da-vinci-dit-l%C3%A9onard-de-vinci-1452-1519-paris-mus%C3%A9e-du-louvre/uQGZ28lYUJ3OGw')

INSERT INTO Artists(ArtistName, ArtistWikiPage) VALUES
('Vincent Van Gogh', N'https://artsandculture.google.com/project/van-gogh');

SET @ArtistID = @@IDENTITY;

INSERT INTO Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
(@ArtistID, 'The Starry Night', 1889, N'Van Gogh’s night sky is a field of roiling energy. Below the exploding stars, the village is a place of quiet order.', 
        N'https://artsandculture.google.com/asset/the-starry-night-vincent-van-gogh/bgEuwDxel93-Pg'),
(@ArtistID, 'Sunflowers', 1889, N'Van Gogh’s paintings of Sunflowers are among his most famous. He did them in Arles, in the south of France, in 1888 and 1889.',
        N'https://artsandculture.google.com/asset/sunflowers-vincent-van-gogh/hwEGmsM-FoHAwA');
        
INSERT INTO Artists(ArtistName, ArtistWikiPage) VALUES
('Claude Monet', N'https://artsandculture.google.com/project/monetwashere');

SET @ArtistID = @@IDENTITY;

INSERT INTO Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
(@ArtistID, 'The Thames at Westminster', 1871, N'En otoño de 1870, París estaba asediada durante la guerra franco-prusiana, y Monet huyó de Francia con su joven familia. Se instalaron en Londres, donde pintó esta vista brumosa del Támesis la primavera siguiente.', N'https://artsandculture.google.com/story/VwKyDFXzPLGdIg');

INSERT INTO dbo.Artists(ArtistName, ArtistWikiPage) VALUES
('Pablo Picasso', N'https://artsandculture.google.com/entity/pablo-picasso/m060_7?categoryId=artist')

SET @ArtistID = @@IDENTITY;

INSERT INTO dbo.Artworks(ArtistId, ArtworkName, ArtworkYear, ArtworkDescription, ArtworkURL) VALUES 
(@ArtistID, 'Toros', 1960, N'Pintura hecha por Picasso en 1960', N'https://artsandculture.google.com/asset/toros-illustration-1960-pablo-ruiz-picasso/OQHCYysg78-c5w')
        
SELECT * FROM Artists;
SELECT * FROM Artworks;