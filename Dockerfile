FROM mcr.microsoft.com/dotnet/sdk:9.0 as base

ARG PROJECT="Orso.Arpa.Api"
ARG ENVIRONMENT="Development"
ARG TARGETPLATFORM

ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV MAIN_PROJECT="./$PROJECT"
ENV DOTNET_ENVIRONMENT=$ENVIRONMENT

# Install libgdiplus using the appropriate package manager based on architecture
RUN if [ "$TARGETPLATFORM" = "linux/arm64" ]; then \
        apt-get update && apt-get install -y libgdiplus; \
    else \
        apt-get update && apt-get install -y libgdiplus; \
    fi
#/usr/lib/libgdiplus.so

WORKDIR /home/app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

WORKDIR /home/app/Tests
COPY ./Tests/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

WORKDIR /home/app
RUN dotnet restore

COPY . .

EXPOSE 5000

# ENTRYPOINT dotnet watch -p ${MAIN_PROJECT} run --urls "http://0.0.0.0:5000"
ENTRYPOINT dotnet run -p ${MAIN_PROJECT} --urls "http://0.0.0.0:5000"

FROM base as test

WORKDIR /home/app

ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM base as builder

ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /home/app

RUN dotnet publish ./Orso.Arpa.Api/Orso.Arpa.Api.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /publish

COPY --from=builder /publish .

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"
ENV DOTNET_ENVIRONMENT="RaspberryPi" 
ENV ASPNETCORE_ENVIRONMENT="RaspberryPi"

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
