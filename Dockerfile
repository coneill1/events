FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY Events.API/*.csproj ./Events.API/
RUN dotnet restore

# copy everything else and build app
COPY Events.API/. ./Events.API/
WORKDIR /app/Events.API
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/Events.API/out ./
ENTRYPOINT ["dotnet", "Events.API.dll"]