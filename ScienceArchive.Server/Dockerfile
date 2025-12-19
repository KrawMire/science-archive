# Build solution stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /backend

# Copy files and build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /publish

# Copy files from "Build stage"
COPY --from=build /publish .
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "ScienceArchive.Rest.Api.dll"]