# Build solution stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /backend

# Copy files and build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish

# Copy files from "Build stage"
COPY --from=build /publish .
EXPOSE 80

# Set up environment variables
ARG POSTGRESQL_CONNECTION_STRING
ENV POSTGRESQL_CONNECTION_STRING=$POSTGRESQL_CONNECTION_STRING

ENTRYPOINT ["dotnet", "ScienceArchive.Api.dll"]
