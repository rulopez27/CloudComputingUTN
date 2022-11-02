/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [CloudComputingUTN.DataBase]
GO

DECLARE @ArtistID INT = 0

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

GO