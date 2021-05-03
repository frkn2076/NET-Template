#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["LogQueue/LogQueue.Consumer/LogQueue.Consumer.csproj", "LogQueue/LogQueue.Consumer/"]
RUN dotnet restore "LogQueue/LogQueue.Consumer/LogQueue.Consumer.csproj"
COPY . .
WORKDIR "/src/LogQueue/LogQueue.Consumer"
RUN dotnet build "LogQueue.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LogQueue.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogQueue.Consumer.dll"]