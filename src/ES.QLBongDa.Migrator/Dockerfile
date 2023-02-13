#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ES.QLBongDa.Migrator/ES.QLBongDa.Migrator.csproj", "src/ES.QLBongDa.Migrator/"]
COPY ["src/ES.QLBongDa.EntityFrameworkCore/ES.QLBongDa.EntityFrameworkCore.csproj", "src/ES.QLBongDa.EntityFrameworkCore/"]
COPY ["src/ES.QLBongDa.Core/ES.QLBongDa.Core.csproj", "src/ES.QLBongDa.Core/"]
COPY ["src/ES.QLBongDa.Core.Shared/ES.QLBongDa.Core.Shared.csproj", "src/ES.QLBongDa.Core.Shared/"]
RUN dotnet restore "src/ES.QLBongDa.Migrator/ES.QLBongDa.Migrator.csproj"
COPY . .
WORKDIR "/src/src/ES.QLBongDa.Migrator"
RUN dotnet build "ES.QLBongDa.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ES.QLBongDa.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ES.QLBongDa.Migrator.dll", "-s"]
