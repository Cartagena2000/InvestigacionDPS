Investigacion aplicada 2 DPS 
Integrantes:
-Jose Angel Cartagena Rivera CR190362
-Jeanluca Chavez Flores CF190725
-Karla Lisette Mejia Ortiz MO190663
-Angel Guillermo Sanchez mangandi SM192656

■ Instrucciones claras sobre cómo configurar y construir el 
contenedor Docker de la API.

Para contruir el contenedor en Visual Studio con asp.net 8.0 es relativamente sencillo ya que con el siguiente docker file el creara 
automaticamente la imagen y el contenedor en Docker al ejecutar la api en Visual Studio sin necesidad de utilizar ningun comando para la creacion de estos.

Dockerfile
#Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

#Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


#Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TorneoAPI/TorneoAPI.csproj", "TorneoAPI/"]
RUN dotnet restore "./TorneoAPI/TorneoAPI.csproj"
COPY . .
WORKDIR "/src/TorneoAPI"
RUN dotnet build "./TorneoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TorneoAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TorneoAPI.dll"]

■ Comandos para ejecutar y desplegar la API utilizando Docker.

Con el siguiente comando podemos verificar la imagenes de docker creadas:

docker images

Con el siguiente comando podemos verificar que el contenedor este corriendo:
(El contenedor va aparecer cuando ejecutemos el proyecto en visual Studio, el lo crea automaticamente al realizar la ejecución)

docker ps

Con el siguiente comando podemos ejecutar nuestro contenedor Docker manualmente, por si queremos unicamente consultar la api a traves de otra app por ejemplo postman:

docker start TorneoAPI

Con este otro comando podemos detener nuestro contenedor:

docker stop TorneoAPI

■ Pasos para probar la funcionalidad de la API dentro del contenedor 
Docker.

■ Funcionamiento de la API

Una vez iniciada la API podemos verificar su funcionamiento, empezamos agregando un “Equipo” con el Método POST, llenamos los espacios: "id": 0, y "nombre": "string", Podemos asignar un id cualquiera para el equipo, también podemos ver los Equipos agregados en la Base con el método GET. 

Para agregar un “jugador” igual tenemos que utilizar el método POST, Agregamos un id para el jugador, y también tenemos que agregar el id del equipo al que pertenece, además del nombre y la edad del jugador. 

Para la Tabla de “Partidos” vamos a utilizar los id de los dos equipos que se enfrentan, también la ahora y fecha en la que se va a jugar el partido. También se deberá agregar un id propio al partido, este se va a relaciona con la tabla de Resultados. 

Para los “Resultados” utilizaremos del id del partido, y vamos a agregar los goles del equipo visitante y del equipo local, además podemos escribir algunas observaciones del partido. 

NOTA: Todas las Tablas tienen los métodos DELET Y PUT por id que sirven para borrar o editar un dato especifico de la tabla. 
