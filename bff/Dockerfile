FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy solution and central package file first to leverage Docker cache for restore
COPY ["ProjectFollowUp.slnx", "./"]
COPY ["Directory.Packages.props", "./"]

# Copy project files to allow restore to be cached when project files don't change
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["tests/WebApi.IntegrationTests/WebApi.IntegrationTests.csproj", "tests/WebApi.IntegrationTests/"]

RUN dotnet restore "ProjectFollowUp.slnx"

# Copy the rest of the repository
COPY . .

# Publish the WebApi project
FROM build AS publish
WORKDIR /src
RUN dotnet publish "./src/WebApi/WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore

# Final image: use a slimmer runtime base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Expose the same ports as before
EXPOSE 8080

# Copy the published output and set entrypoint
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjectFollowUp.BFF.WebApi.dll"]