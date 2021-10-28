# First stage
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /DockerSource

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY Pruefungszeugnis/*.csproj ./Pruefungszeugnis/
RUN dotnet restore

# Copy everything else and build website
COPY ./Pruefungszeugnis/ ./Pruefungszeugnis/
WORKDIR /DockerSource/Pruefungszeugnis
RUN dotnet publish -c release -o /DockerOutput/Website

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt-get update && apt-get install -y libgdiplus libc6-dev
RUN ln -s /lib/x86_64-linux-gnu/libdl.so.2 /lib/x86_64-linux-gnu/libdl.so
RUN ln -s /usr/lib/libgdiplus.so /lib/x86_64-linux-gnu/libgdiplus.so
WORKDIR /DockerOutput/Website
COPY --from=build /DockerOutput/Website ./
# ENTRYPOINT ["/bin/bash"]
ENTRYPOINT ["dotnet", "Pruefungszeugnis.dll"]