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

Te devolvera algo similar a lo siguiente:
REPOSITORY                        TAG       IMAGE ID       CREATED        SIZE
torneoapi                         dev       d907cbd9318e   24 hours ago   217MB
mcr.microsoft.com/dotnet/aspnet   8.0       ebf2935462ac   6 days ago     217MB

Con el siguiente comando podemos verificar que el contenedor este corriendo:
(el contenedor va aparecer cuando ejecutemos el proyecto en visual Studio, si detenemos la ejecución el contenedor desaparecera)

docker ps

Te devolvera algo similar a lo siguiente:

CONTAINER ID   IMAGE           COMMAND                  CREATED         STATUS         PORTS                                              NAMES
b429ef4b5d05   torneoapi:dev   "dotnet --roll-forwa…"   2 seconds ago   Up 2 seconds   0.0.0.0:32774->8080/tcp, 0.0.0.0:32775->8081/tcp   TorneoAPI


■ Pasos para probar la funcionalidad de la API dentro del contenedor 
Docker.
