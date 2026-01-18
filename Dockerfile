FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base

ARG PROJECT="Orso.Arpa.Api"
ARG ENVIRONMENT="Development"
ARG TARGETPLATFORM

ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV MAIN_PROJECT="./$PROJECT"
ENV DOTNET_ENVIRONMENT=$ENVIRONMENT

# Install libgdiplus for System.Drawing support (required for image processing)
RUN apt-get update && apt-get install -y libgdiplus && rm -rf /var/lib/apt/lists/*
#/usr/lib/libgdiplus.so

WORKDIR /home/app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in ./*.csproj; do mkdir -p "./${file%.*}/" && mv "$file" "./${file%.*}/"; done

WORKDIR /home/app/Tests
COPY ./Tests/*/*.csproj ./
RUN for file in ./*.csproj; do mkdir -p "./${file%.*}/" && mv "$file" "./${file%.*}/"; done

WORKDIR /home/app
RUN dotnet restore

COPY . .

# Create DummyImageProvider if it doesn't exist (fallback for Raspberry Pi)
RUN mkdir -p /home/app/Orso.Arpa.Api/Middleware && \
    if [ ! -f /home/app/Orso.Arpa.Api/Middleware/DummyImageProvider.cs ]; then \
    echo 'using System; \n\
using System.IO; \n\
using System.Threading.Tasks; \n\
using Microsoft.AspNetCore.Http; \n\
using SixLabors.ImageSharp.Web.Middleware; \n\
using SixLabors.ImageSharp.Web.Providers; \n\
using SixLabors.ImageSharp.Web.Resolvers; \n\
\n\
namespace Orso.Arpa.Api.Middleware \n\
{ \n\
    /// <summary> \n\
    /// A dummy image provider that does not attempt to resolve any images. \n\
    /// Used as a fallback when the PhysicalFileSystemProvider cannot find wwwroot. \n\
    /// </summary> \n\
    public class DummyImageProvider : IImageProvider \n\
    { \n\
        /// <inheritdoc/> \n\
        public bool IsValidRequest(HttpContext context) => false; \n\
\n\
        /// <inheritdoc/> \n\
        public Task<IImageResolver> GetAsync(HttpContext context) => Task.FromResult<IImageResolver>(null); \n\
\n\
        /// <inheritdoc/> \n\
        public ProcessingBehavior GetProcessingBehavior(HttpContext context) => ProcessingBehavior.CommandOnly; \n\
\n\
        /// <inheritdoc/> \n\
        public Func<HttpContext, bool> Match { get; set; } = _ => false; \n\
    } \n\
}' > /home/app/Orso.Arpa.Api/Middleware/DummyImageProvider.cs; \
    fi

EXPOSE 5000

# ENTRYPOINT dotnet watch -p ${MAIN_PROJECT} run --urls "http://0.0.0.0:5000"
ENTRYPOINT ["sh", "-c", "dotnet run -p ${MAIN_PROJECT} --urls http://0.0.0.0:5000"]

FROM base AS test

WORKDIR /home/app

ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM base AS builder

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /home/app

RUN dotnet publish ./Orso.Arpa.Api/Orso.Arpa.Api.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Install curl for health checks
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

WORKDIR /publish

COPY --from=builder /publish .

# Environment can be overridden at build time or runtime
# Default to RaspberryPi for backward compatibility
ARG ASPNETCORE_ENV=RaspberryPi
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENV DOTNET_ENVIRONMENT="${ASPNETCORE_ENV}"
ENV ASPNETCORE_ENVIRONMENT="${ASPNETCORE_ENV}"

# Create an entrypoint script
RUN echo '#!/bin/bash\n\
echo "Starting ARPA API..."\n\
echo "Environment: $ASPNETCORE_ENVIRONMENT"\n\
echo "Creating required directories..."\n\
# Create storage directory\n\
mkdir -p /publish/storage/imagecache\n\
chmod -R 755 /publish/storage\n\
# Create wwwroot directory if it doesn't exist\n\
mkdir -p /publish/wwwroot\n\
chmod -R 755 /publish/wwwroot\n\
# Create a test image to ensure the directory is not empty\n\
touch /publish/wwwroot/test.png\n\
echo "Directory structure:"\n\
find /publish -type d | sort\n\
echo "Starting application..."\n\
exec dotnet Orso.Arpa.Api.dll\n\
' > /publish/entrypoint.sh && \
chmod +x /publish/entrypoint.sh

ENTRYPOINT ["/publish/entrypoint.sh"]
