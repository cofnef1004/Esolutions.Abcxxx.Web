#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ES.QLBongDa.Web.Mvc/ES.QLBongDa.Web.Mvc.csproj", "src/ES.QLBongDa.Web.Mvc/"]
COPY ["src/ES.QLBongDa.Web.Core/ES.QLBongDa.Web.Core.csproj", "src/ES.QLBongDa.Web.Core/"]
COPY ["src/ES.QLBongDa.Application/ES.QLBongDa.Application.csproj", "src/ES.QLBongDa.Application/"]
COPY ["src/ES.QLBongDa.Application.Shared/ES.QLBongDa.Application.Shared.csproj", "src/ES.QLBongDa.Application.Shared/"]
COPY ["src/ES.QLBongDa.Core.Shared/ES.QLBongDa.Core.Shared.csproj", "src/ES.QLBongDa.Core.Shared/"]
COPY ["src/ES.QLBongDa.Core/ES.QLBongDa.Core.csproj", "src/ES.QLBongDa.Core/"]
COPY ["src/ES.QLBongDa.EntityFrameworkCore/ES.QLBongDa.EntityFrameworkCore.csproj", "src/ES.QLBongDa.EntityFrameworkCore/"]
COPY ["src/ES.QLBongDa.GraphQL/ES.QLBongDa.GraphQL.csproj", "src/ES.QLBongDa.GraphQL/"]
RUN dotnet restore "src/ES.QLBongDa.Web.Mvc/ES.QLBongDa.Web.Mvc.csproj"
COPY . .
WORKDIR "/src/src/ES.QLBongDa.Web.Mvc"
RUN dotnet build "ES.QLBongDa.Web.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ES.QLBongDa.Web.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ES.QLBongDa.Web.Mvc.dll"]
