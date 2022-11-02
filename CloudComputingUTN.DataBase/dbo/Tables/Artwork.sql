CREATE TABLE [dbo].[Artwork]
(
	[ArtworkId] INT NOT NULL PRIMARY KEY IDENTITY,
	ArtistId INT NOT NULL REFERENCES Artists(ArtistId),
	ArtworkName VARCHAR(100) NOT NULL,
	ArtworkDate DATE NULL,
	ArtworkDescription VARCHAR(300),
	ArtworkURL VARCHAR(MAX)
)