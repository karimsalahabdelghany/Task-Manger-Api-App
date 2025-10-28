# Use official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY *.sln .
COPY TaskManager.API/*.csproj TaskManager.API/
RUN dotnet restore TaskManager.API/TaskManager.API.csproj

# Copy everything else and build
COPY . .
WORKDIR /src/TaskManager.API
RUN dotnet publish -c Release -o /app

# Use ASP.NET runtime image for final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]
