#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG ENV

WORKDIR /src
COPY ["Gateway/Gateway.API/Gateway.API.csproj", "Gateway/Gateway.API/"]
COPY ["Infra/Constants/Infra.Constants/Infra.Constants.csproj", "Infra/Constants/Infra.Constants/"]
COPY ["Infra/LogPublisher/Infra.LogPublisher.csproj", "Infra/LogPublisher/"]
COPY ["Infra/Extensions/Infra.Extensions/Infra.Extensions.csproj", "Infra/Extensions/Infra.Extensions/"]
COPY ["Infra/Localization/Infra.Localizer.DataAccess/Infra.Localizer.DataAccess.csproj", "Infra/Localization/Infra.Localizer.DataAccess/"]
COPY ["Infra/Core/Infra.Core.DataAccess/Infra.Core.DataAccess.csproj", "Infra/Core/Infra.Core.DataAccess/"]
RUN dotnet restore "Gateway/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/Gateway/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c $ENV -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c $ENV -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]