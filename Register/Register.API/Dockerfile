#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Register/Register.API/Register.API.csproj", "Register/Register.API/"]
COPY ["Register/Register.Business/Register.Business.csproj", "Register/Register.Business/"]
COPY ["Register/Register.Repository/Register.Repository.csproj", "Register/Register.Repository/"]
COPY ["Register/Register.DataAccess/Register.DataAccess.csproj", "Register/Register.DataAccess/"]
RUN dotnet restore "Register/Register.API/Register.API.csproj"
COPY . .
WORKDIR "/src/Register/Register.API"
RUN dotnet build "Register.API.csproj" -c PROD -o /app/build

FROM build AS publish
RUN dotnet publish "Register.API.csproj" -c PROD -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]