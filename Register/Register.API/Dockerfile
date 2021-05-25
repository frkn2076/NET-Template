#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

ARG ENV

WORKDIR /src
COPY ["Register/Register.API/Register.API.csproj", "Register/Register.API/"]
COPY ["Infra/Constants/Infra.Constants/Infra.Constants.csproj", "Infra/Constants/Infra.Constants/"]
COPY ["Infra/Helper/Infra.Helper/Infra.Helper.csproj", "Infra/Helper/Infra.Helper/"]
COPY ["Register/Register.Business/Register.Business.csproj", "Register/Register.Business/"]
COPY ["Register/Register.Repository/Register.Repository.csproj", "Register/Register.Repository/"]
COPY ["Register/Register.DataAccess/Register.DataAccess.csproj", "Register/Register.DataAccess/"]
COPY ["Infra/Core/Infra.Core.DataAccess/Infra.Core.DataAccess.csproj", "Infra/Core/Infra.Core.DataAccess/"]
COPY ["Infra/Localization/Infra.Localizer.Repository/Infra.Localizer.Repository.csproj", "Infra/Localization/Infra.Localizer.Repository/"]
COPY ["Infra/Localization/Infra.Localizer.DataAccess/Infra.Localizer.DataAccess.csproj", "Infra/Localization/Infra.Localizer.DataAccess/"]
COPY ["Infra/Localization/Infra.Localizer/Infra.Localizer.csproj", "Infra/Localization/Infra.Localizer/"]
COPY ["Infra/CommonMessages/Infra.CommonMessages/Infra.CommonMessages.csproj", "Infra/CommonMessages/Infra.CommonMessages/"]
COPY ["Infra/Extensions/Infra.Extensions/Infra.Extensions.csproj", "Infra/Extensions/Infra.Extensions/"]
RUN dotnet restore "Register/Register.API/Register.API.csproj"
COPY . .
WORKDIR "/src/Register/Register.API"
RUN dotnet build "Register.API.csproj" -c $ENV -o /app/build

FROM build AS publish
RUN dotnet publish "Register.API.csproj" -c $ENV -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]