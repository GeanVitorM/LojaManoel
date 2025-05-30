FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY LojaManoel.IdentityServer/LojaManoel.IdentityServer.csproj LojaManoel.IdentityServer/
RUN dotnet restore LojaManoel.IdentityServer/LojaManoel.IdentityServer.csproj
COPY . .
WORKDIR /src/LojaManoel.IdentityServer
RUN dotnet build LojaManoel.IdentityServer.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish LojaManoel.IdentityServer.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LojaManoel.IdentityServer.dll"]