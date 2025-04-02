FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files and restore dependencies
COPY ["Vendas.sln", "./"]
COPY ["API/API.csproj", "API/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR /src
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

# Set timezone to Sao Paulo
ENV TZ=America/Sao_Paulo

EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "API.dll"] 