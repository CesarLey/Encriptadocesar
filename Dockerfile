# Dockerfile para CaesarApi
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CaesarApi.csproj", "."]
RUN dotnet restore "./CaesarApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "CaesarApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CaesarApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://*:${PORT}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "CaesarApi.dll"] 