USE MuseumDb;

CREATE TABLE Artists
(
    ArtistId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ArtistName VARCHAR(200),
    ArtistWikiPage VARCHAR(65535)
);

CREATE UNIQUE INDEX IX_ArtistName ON Artists(ArtistName);

CREATE TABLE Artworks
(
    ArtworkId INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ArtistId INT NOT NULL,
    ArtworkName VARCHAR(200) NOT NULL,
    ArtworkYear INT,
    ArtworkDescription VARCHAR(300),
    ArtworkURL VARCHAR(65535),
    FOREIGN KEY (ArtistId) REFERENCES Artists(ArtistID) ON DELETE CASCADE
);

CREATE UNIQUE INDEX IX_ArtworkName ON Artworks(ArtworkName);

SELECT @ArtistID;

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
        
SELECT * FROM Artists;
SELECT * FROM Artworks;