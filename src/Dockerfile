FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build

WORKDIR /app
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["GoldenRaspberryAwardsApi/GoldenRaspberryAwardsApi.csproj", "GoldenRaspberryAwardsApi/"]
RUN dotnet restore "./GoldenRaspberryAwardsApi/GoldenRaspberryAwardsApi.csproj"

COPY . .
RUN rm -rf ./GoldenRaspberryAwardsApi/appsettings*

WORKDIR "/src/GoldenRaspberryAwardsApi"
RUN dotnet build "./GoldenRaspberryAwardsApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GoldenRaspberryAwardsApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0.1

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "GoldenRaspberryAwardsApi.dll"]