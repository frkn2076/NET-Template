#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Gateway/Gateway.API/Gateway.API.csproj", "Gateway/Gateway.API/"]
COPY ["Gateway/Gateway.Middleware/Gateway.Middleware.csproj", "Gateway/Gateway.Middleware/"]
COPY ["Gateway/Gateway.LogPublisher/Gateway.LogPublisher.csproj", "Gateway/Gateway.LogPublisher/"]
RUN dotnet restore "Gateway/Gateway.API/Gateway.API.csproj"
COPY . .
WORKDIR "/src/Gateway/Gateway.API"
RUN dotnet build "Gateway.API.csproj" -c $ENVIRONMENT -o /app/build
RUN dotnet build "Gateway.API.csproj" -c $ENVIRONMENT -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.API.csproj" -c $ENVIRONMENT -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.API.dll"]