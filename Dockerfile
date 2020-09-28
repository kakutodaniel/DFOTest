
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder

COPY DFO.Test.API ./DFO.Test.API
COPY DFO.Test.Application ./DFO.Test.Application
COPY DKakuto.DFO.Test.sln ./DKakuto.DFO.Test.sln

RUN dotnet restore --no-cache --source https://api.nuget.org/v3/index.json

WORKDIR /DFO.Test.API

RUN dotnet publish --output /app/ -c Release --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=builder /app .

EXPOSE 80/tcp
ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet", "DFO.Test.API.dll"]