USE [master]
GO
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
GO

CREATE LOGIN [museumDb_Developer] WITH PASSWORD=N'$tr0ng_p4$$w0rd', DEFAULT_DATABASE=MuseumDb, CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
use [master];
GO
USE [MuseumDb]
GO
CREATE USER [museumDb_Developer] FOR LOGIN [museumDb_Developer]
GO