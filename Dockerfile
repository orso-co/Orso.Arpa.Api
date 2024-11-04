FROM mcr.microsoft.com/dotnet/sdk:9.0 as base

ARG PROJECT="Orso.Arpa.Api"
ARG ENVIRONMENT="Development"

ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV MAIN_PROJECT="./$PROJECT"

RUN apt-get update && apt-get install -y libgdiplus
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

FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine

WORKDIR /publish

COPY --from=builder /publish .

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

ENTRYPOINT ["dotnet", "Orso.Arpa.Api.dll"]
