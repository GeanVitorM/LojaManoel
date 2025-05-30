FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["LojaManoel.API.csproj", "."]
RUN dotnet restore "./LojaManoel.API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LojaManoel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LojaManoel.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LojaManoel.API.dll"]