FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY ./Business/Business.csproj ./Business/
COPY ./Core/Core.csproj ./Core/
COPY ./DataAccess/DataAccess.csproj ./DataAccess/
COPY ./Entities/Entities.csproj ./Entities/
COPY ./WebAPI/WebAPI.csproj ./WebAPI/
COPY ./WebAPI.Test/WebAPI.Test.csproj ./WebAPI.Test/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet test ./WebAPI.Test/*.csproj
RUN dotnet publish ./WebAPI/*.csproj -o /publish/

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as base
WORKDIR /src
COPY --from=build /publish .
EXPOSE 80
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet","WebAPI.dll" ]