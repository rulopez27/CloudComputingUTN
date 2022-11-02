CREATE TABLE [dbo].[Artworks]
(
	[ArtworkId] INT NOT NULL PRIMARY KEY IDENTITY,
	ArtistId INT NOT NULL REFERENCES Artists(ArtistId),
	ArtworkName VARCHAR(100) NOT NULL,
	ArtworkYear INT NULL,
	ArtworkDescription VARCHAR(300),
	ArtworkURL VARCHAR(MAX)
)