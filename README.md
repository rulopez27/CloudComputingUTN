# Cloud Computing

El prop&oacute;sito de este proyecto es demostrar una aplicaci&oacute;n Web MVC con conexiones a bases de datos de SQL Server, MySQL (tanto locales como en Amazon Web Services RDS) as&iacute; como SQLite de manera local usando Entity Framework Core para cualquier motor de base de datos. 
-   [Recomendaciones](#recomendaciones).
-   [Componentes de la Soluci&oacute;n](#componentes-de-la-solución).
-   [Instrucciones](#instrucciones).


## Recomendaciones
El proyecto est&aacute; hecho en .NET 7, por lo que es importante instalar tanto el SDK como el ASP.NET Core Runtime antes de ejecutarlo si no cuentas con ello. Puedes verificar si tienes el SDK instalado ejecutando el siguiente comando en la terminal o Powershell:

```
dotnet --info
```

En caso de estar instalado, el comando regresar&aacute; algo parecido a esto:\
![Respuesta del comando dotnet --info en PowerShell](readme/dotnet_info_output.png)



En caso de no tener una respuesta similar a la anterior:
-   Puedes [descargar .NET 7.0](https://dotnet.microsoft.com/es-es/download/dotnet/7.0).
-   O la [versi&oacute;n recomendada por Microsoft (7.0 o superior)](https://dotnet.microsoft.com/es-es/download).

Opcional, tambi&eacute;n se recomienda instalar las siguientes herramientas/software para facilitar el desarrollo si no cuentas con ellas a&uacute;n:
-   [Microsoft SQL Server Developer](https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x80a&culture=es-mx&country=mx).
-   [Microsoft SQL Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16#download-ssms).
-   [MySQL Community Server](https://dev.mysql.com/downloads/mysql/).
-   [MySQL Workbench](https://dev.mysql.com/downloads/workbench/).
-   [SQLectron](https://sqlectron.github.io/).
-   [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/es/vs/community/).
-   [Visual Studio Code](https://code.visualstudio.com/download).
-   [Git](https://git-scm.com/downloads).

## Componentes de la Soluci&oacute;n

La soluci&oacute;n se compone de distintos proyectos individuales:
-   [CloudComputingUTN.DataAccessLayer](#cloudcomputingutndataaccesslayer)
-   [CloudComputingUTN.DataBase](#cloudcomputingutndatabase).
-   [CloudComputingUTN.Entities](#cloudcomputingutnentities).
-   [CloudComputingUTN.Middleware](#cloudcomputingutnmiddleware).
-   [CloudComputingUTN.Middleware.UnitTests](#cloudcomputingutnmiddlewareunittests).
-   [CloudComputingUTN.MySQL](#cloudcomputingutnmysql).
-   [CloudComputingUTN.Service](#cloudcomputingutnservice).
-   [CloudComputingUTN.Service.UnitTests](#cloudcomputingutnserviceunittests).
-   [CloudComputingUTN.Sqlite](#cloudcomputingutnsqlite).
-   [CloudComputingUTN.WebApp](#cloudcomputingutnwebapp).

### CloudComputingUTN.DataAccessLayer
Este proyecto es una librer&iacute;a de clases para formar los DTO (Data Transfer Object) de las entidades.

### CloudComputingUTN.DataBase
Este proyecto es de tipo base de datos SQL Server. Desde aqu&iacute; se puede publicar la base de datos a cualquier servidor SQL Server.

### CloudComputingUTN.Entities
Este proyecto contiene las entidades que representan las tablas de la base de datos.

### CloudComputingUTN.Middleware
Este proyecto contiene el contexto de EF Core, as&iacute; como el repositorio y la implementaci&oacute;n del repositorio de m&eacute;todos y funciones de interacci&oacute; con la base de datos.

### CloudComputingUTN.Middleware.UnitTests
Este proyecto contiene las pruebas unitarias del repositorio de m&eacute;todos y funciones de interacci&oacute;n con la base de datos.

### CloudComputingUTN.MySQL
Este proyecto contiene los scripts SQL usados para crear la base de datos y configurar el usuario en la base de datos MySQL.

### CloudComputingUTN.Service
Este proyecto representa una RESTful API con Swagger usando los mismos m&eacute;todos y funciones de acceso a la base de datos que CloudComputingUTN.WebApp.

### CloudComputingUTN.Service.UnitTests
Este proyecto contiene las pruebas unitarias de la RESTful API CloudComputingUTN.Service

### CloudComputingUTN.Sqlite
Este proyecto es para la configuraci&oacute;n de una base de datos en memoria de SQLite.

### CloudComputingUTN.WebApp
Este proyecto es el sitio web y API que interact&uacute;a con la base de datos.

## Instrucciones
-   [Configurar Base de datos en SQL Server](#configurar-base-de-datos-en-sql-server).
-   [Configurar Base de datos en MySQL](#configurar-base-de-datos-en-mysql).
-   [Ejecutar en Visual Studio Code](#ejecutar-en-visual-studio-code).

### Configurar Base de datos en SQL Server
Primero, debes ejecutar el script llamado ```MuseumDb.sql``` localizado en el proyecto ```CloudComputingUTN.Database``` en tu instancia de MSSQL. Una vez creada la base de datos de manera satisfactoria, el siguiente paso es ejecutar el script ```SQL_Login_Setup.sql``` para crear el usuario de SQL Server que se usa en la cadena de conexi&oacute;n. Los cambios surtir&aacute;n efectos al reiniciar el servicio de MSSQLSERVER.

### Configurar Base de datos en MySQL
Primero, debes ejecutar el script llamado ```MuseumDB.sql``` localizado el directorio ```CloudComputingUTN.MySQL``` en tu instancia de MySQL. Una vez creada la base de datos de manera satisfactoria, el siguiente paso es ejecutar el script ```UserSetup.sql``` para crear el usuario de SQL Server que se usa en la cadena de conexi&oacute;n.

### Ejecutar en Visual Studio Code
#### Ejecutar CloudComputingUTN.WebApp
```
dotnet run --project CloudComputingUTN.WebApp/CloudComputingUTN.WebApp.csproj
```

#### Ejecutar CloudComputingUTN.Service
```
dotnet run --project CloudComputingUTN.WebApp/CloudComputingUTN.Service.csproj
```