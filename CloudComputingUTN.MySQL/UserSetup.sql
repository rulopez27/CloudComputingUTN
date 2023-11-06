#Crear roles
CREATE ROLE 'museumDb_DeveloperRole';
GRANT ALL ON MuseumDb.* TO 'museumDb_DeveloperRole';
GRANT SELECT ON MuseumDb.* TO 'museumDb_DeveloperRole';
GRANT INSERT, UPDATE, DELETE ON MuseumDb.* TO 'museumDb_DeveloperRole';

#Crear usuario
CREATE USER 'museumDb_Developer'@'localhost'
IDENTIFIED BY '$tr0ng_p4$$w0rd'
DEFAULT ROLE 'museumDb_DeveloperRole';