FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FunBooksAndVideos.Api/FunBooksAndVideos.Api.csproj", "FunBooksAndVideos.Api/"]
RUN dotnet restore "FunBooksAndVideos.Api/FunBooksAndVideos.Api.csproj"
COPY . .
WORKDIR "/src/FunBooksAndVideos.Api"
RUN dotnet build "FunBooksAndVideos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunBooksAndVideos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FunBooksAndVideos.Api.dll"]
