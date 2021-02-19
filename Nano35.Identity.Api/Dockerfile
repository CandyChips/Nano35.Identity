FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore Nano35.Identity.Api.csproj --configfile ./NuGet.config
WORKDIR /src/.
RUN dotnet build Nano35.Identity.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Nano35.Identity.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nano35.Identity.Api.dll"]