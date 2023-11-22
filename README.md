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
-	[Solution Items](#solutionitems)
-   [CloudComputingUTN.MySQL](#cloudcomputingutnmysql).
-   [CloudComputingUTN.SQLServer](#cloudcomputingutnsqlserver).
-   [CloudComputingUTN.DataAccessLayer](#cloudcomputingutndataaccesslayer)
-   [CloudComputingUTN.Entities](#cloudcomputingutnentities).
-   [CloudComputingUTN.Middleware](#cloudcomputingutnmiddleware).
-   [CloudComputingUTN.Middleware.UnitTests](#cloudcomputingutnmiddlewareunittests).
-   [CloudComputingUTN.Service](#cloudcomputingutnservice).
-   [CloudComputingUTN.Service.UnitTests](#cloudcomputingutnserviceunittests).
-   [CloudComputingUTN.Sqlite](#cloudcomputingutnsqlite).
-   [CloudComputingUTN.WebApp](#cloudcomputingutnwebapp).


### Solution Items
Este directorio contiene archivos para configurar el control de versiones, así como el README del proyecto y archivos para la publicación en AWS.

-	```.gitattributes```: Configuración de atributos para Git.
-	```.gitignore```: Configuración sobre qu&eacute; extensiones o directorios ignorar del control de versiones.
-	```Procfile```: Configuración de proyecto para desplegarse en una instancia Windows Server con IIS.
-	```aws-windows-deployment-manifest.json```: Configuración de publicaci&oacute;n de sitios web en Windows Server con IIS para los proyectos CloudComputingUTN.WebApp y CloudComputingUTN.Service
-	```buildspec.yml```: Indicaciones para la compilación del proyecto en AWS CodeBuild. Para poder ejecutar un pipeline de manera satisfactoria hay que modificar este archivo para usar el bucket de S3 correcto donde se guardarán los artefactos y la pol&iacute;tica de permisos en el bucket cambiando el cifrado de AWS KMS a SSE AES256.
-	```README.md```: Archivo l&eacute;eme (este documento).

### CloudComputingUTN.SQLServer
Este directorio contiene dos scripts de SQL para crear la base de datos y crear un usuario de SQL para la conexión con el proyecto.

### CloudComputingUTN.MySQL
Este directorio contiene los scripts SQL usados para crear la base de datos y configurar el usuario en la base de datos MySQL.

### CloudComputingUTN.DataAccessLayer
Este proyecto es una librer&iacute;a de clases para formar los DTO (Data Transfer Object) de las entidades.

### CloudComputingUTN.Entities
Este proyecto contiene las entidades que representan las tablas de la base de datos.

### CloudComputingUTN.Middleware
Este proyecto contiene el contexto de EF Core, as&iacute; como el repositorio y la implementaci&oacute;n del repositorio de m&eacute;todos y funciones de interacci&oacute; con la base de datos.

### CloudComputingUTN.Middleware.UnitTests
Este proyecto contiene las pruebas unitarias del repositorio de m&eacute;todos y funciones de interacci&oacute;n con la base de datos.

### CloudComputingUTN.Service
Este proyecto representa una RESTful API con Swagger usando los mismos m&eacute;todos y funciones de acceso a la base de datos que CloudComputingUTN.WebApp.

### CloudComputingUTN.Service.UnitTests
Este proyecto contiene las pruebas unitarias de la RESTful API CloudComputingUTN.Service

### CloudComputingUTN.Sqlite
Este proyecto es para la configuraci&oacute;n de una base de datos en memoria de SQLite.

### CloudComputingUTN.WebApp
Este proyecto es el sitio web y API que interact&uacute;a con la base de datos.

## Instrucciones
-   [Configurar Base de datos en SQLite](#configurar-base-de-datos-sqlite)
-   [Configurar Base de datos en SQL Server](#configurar-base-de-datos-en-sql-server)
-   [Configurar Base de datos en MySQL](#configurar-base-de-datos-en-mysql).
-   [Cambiar tipo de base da datos](#cambiar-tipo-de-base-de-datos).
-   [Ejecutar en Visual Studio 2022](#ejecutar-en-visual-studio-2022).
-   [Ejecutar en Visual Studio Code](#ejecutar-en-visual-studio-code).
-   [Ejecutar en Terminal](#ejecutar-en-terminal).

### Configurar Base de datos SQLite
Los proyectos ```CloudComputingUTN.WebApp``` y ```CloudComputingUTN.Service``` están configurados de la misma manera. Ambos se ejecutan con una base de datos SQLite en memoria por defecto.

### Configurar Base de datos en SQL Server
Primero, debes ejecutar el script llamado ```MuseumDb.sql``` localizado en el directorio ```CloudComputingUTN.SQLServer``` en tu instancia de MSSQL. Una vez creada la base de datos de manera satisfactoria, el siguiente paso es ejecutar el script ```SQL_Login_Setup.sql``` para crear el usuario de SQL Server que se usa en la cadena de conexi&oacute;n. Los cambios surtir&aacute;n efectos al reiniciar el servicio de MSSQLSERVER.

En caso de ejecutarse en localhost (127.0.0.1) no debe realizarase cambio en la cadena de conexi&oacute;n. 

**En caso contrario debes modificar la propiedad ```Data Source``` de la cadena de conexi&oacute;n MSSQL en los archivos ```appsettings.json``` de los proyectos CloudComputingUTN.WebApp y CloudComputingUTN.Service de esta manera sin corchetes []**:
```
 "ConnectionStrings": {
    "MSSQL": "Data Source=[TU ENDPOINT DE MSSQL EN AWS RDS AQUI];Initial Catalog=MuseumDb;User ID=museumDb_Developer;Pwd=$tr0ng_p4$$w0rd;TrustServerCertificate=True",
	"MySQL": "server=localhost;database=MuseumDb;user=museumDb_Developer;pwd=$tr0ng_p4$$w0rd"
  }
```

### Configurar Base de datos en MySQL
Primero, debes ejecutar el script llamado ```MuseumDB.sql``` localizado el directorio ```CloudComputingUTN.MySQL``` en tu instancia de MySQL. Una vez creada la base de datos de manera satisfactoria, el siguiente paso es ejecutar el script ```UserSetup.sql``` para crear el usuario de SQL Server que se usa en la cadena de conexi&oacute;n.

En caso de ejecutarse en localhost (127.0.0.1) no debe realizarase cambio en la cadena de conexi&oacute;n. 

**En caso contrario debes modificar la propiedad ```server``` de la cadena de conexi&oacute;n MySQL en los archivos ```appsettings.json``` de los proyectos CloudComputingUTN.WebApp y CloudComputingUTN.Service de esta manera sin corchetes []**:
```
 "ConnectionStrings": {
    "MSSQL": "Data Source=localhost;Initial Catalog=MuseumDb;User ID=museumDb_Developer;Pwd=$tr0ng_p4$$w0rd;TrustServerCertificate=True",
    "MySQL": "server=[TU ENDPOINT DE MSSQL EN AWS RDS];database=MuseumDb;user=museumDb_Developer;pwd=$tr0ng_p4$$w0rd"
  }
```

### Cambiar tipo de base de datos
Los proyectos **CloudComputingUTN.WebApp** y **CloudComputingUTN.Service** están configurados de la misma manera. Ambos se ejecutan con una base de datos SQLite en memoria por defecto, pero pueden configurarse para ejecutarse con conexión a SQL Server o MySQL de manera local o en AWS RDS mediante s&iacute;mbolos condicionales de compilaci&oacute;n.

- [Modificar los s&iacute;bolos condicionales de compilaci&oacute;n en los archivos .csproj](#modificar-los-símbolos-condicionales-de-compilación-en-los-archivos-csproj)
- [Modificar los s&iacute;bolos condicionales de compilaci&oacute;n en los archivos .csproj](#modificar-símbolos-condicionales-de-compilación-en-visual-studio-2022)

#### Modificar los s&iacute;mbolos condicionales de compilaci&oacute;n en los archivos .csproj

Por defecto, ambos archivos ```CloudComputingUTN.WebApp.csproj``` y ```CloudComputingUTN.Serivice.csproj``` **tienen esta configuraci&oacute;n que usar&aacute; SQLite como base de datos**:

```
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
	<DefineConstants>$(DefineConstants);DATABASE_ENGINE_SQLITE</DefineConstants>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
	  <DefineConstants>$(DefineConstants);DATABASE_ENGINE_SQLITE</DefineConstants>
  </PropertyGroup>
```

Para cambiar a **Microsoft SQL Server** debes remplazar ambos bloques  ```PropertyGroup``` por los siguientes:
```
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
	<DefineConstants>$(DefineConstants);DATABASE_ENGINE_MSSQL</DefineConstants>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
	  <DefineConstants>$(DefineConstants);DATABASE_ENGINE_MSSQL</DefineConstants>
  </PropertyGroup>
```
Para cambiar a **MySQL** debes remplazar ambos bloques ```PropertyGroup``` por los siguientes:
```
  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
	<DefineConstants>$(DefineConstants);DATABASE_ENGINE_MYSQL</DefineConstants>
  </PropertyGroup>

  <PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
	  <DefineConstants>$(DefineConstants);DATABASE_ENGINE_MYSQL</DefineConstants>
  </PropertyGroup>
```

#### Modificar S&iacute;mbolos Condicionales de Compilaci&oacute;n en Visual Studio 2022
Por defecto, ambos proyectos  ```CloudComputingUTN.WebApp.csproj``` y ```CloudComputingUTN.Serivice.csproj``` **est&aacute;n configurados para usar SQLite como base de datos**.
Puedes modificar los s&iacute;mbolos condicionales de compilaci&oacute;n de ambos proyectos CloudComputingUTN.WebApp y CloudComputingUTN.Service haciendo clic derecho en el proyecto y luego en Propiedades > Build > Conditional Compilation Symbols. Deben de llamarse exactamente igual a los especificados anteriormente:
- ```DATABASE_ENGINE_SQLITE```: Base de datos SQLite. Configuraci&oacute;n por defecto.
- ```DATABASE_ENGINE_MYSQL```: Base de datos MySQL.
- ```DATABASE_ENGINE_MYSQL```: Base de datos SQL.

Una vez modificada la condici&oacute;n de compilaci&oacute;n puedes ejecutar de manera normal en Visual Studio o con F5.

### Ejecutar en Visual Studio 2022
Abre la soluci&oacute;n CloudComputingUTN.sln y ejecuta normalmente. Puedes configurar para que se ejecuten los proyectos ```CloudComputingUTN.Service``` y ```CloudComputingUTN.WebApp``` al mismo tiempo.

### Ejecutar en Visual Studio Code
Abre el directorio CloudComputingUTN en Visual Code, selecciona CloudComputingUTN.WebApp.csproj o CloudComputingUTN.Service.csproj y presiona F5 para ejecutar. Tambi&eacute;n puedes hacerlo mediante Run > Start Debugging o Run > Start Without Debugging.

### Ejecutar en Terminal
Abre una terminal en la misma ruta del proyecto y ejecuta los siguientes comandos

#### Restaurar dependencias del proyecto
```
dotnet restore
```

#### Compilar proyecto
```
dotnet build
```

#### Ejecutar Pruebas Unitarias
```
dotnet test
```

##### Ejecutar CloudComputingUTN.WebApp
```
dotnet run --project CloudComputingUTN.WebApp/CloudComputingUTN.WebApp.csproj
```

##### Ejecutar CloudComputingUTN.Service
```
dotnet run --project CloudComputingUTN.WebApp/CloudComputingUTN.Service.csproj
```