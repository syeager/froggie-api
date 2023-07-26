FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS source
WORKDIR /src
COPY . .
WORKDIR /src/Source/Froggie.Api
RUN dotnet restore

FROM source as test
WORKDIR /src
RUN dotnet test -c Debug -o /app/test

FROM source AS publish
WORKDIR /src/Source/Froggie.Api
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM source AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Froggie.Api.dll"]