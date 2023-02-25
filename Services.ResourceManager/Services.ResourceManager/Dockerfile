#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services.ResourceManager/Services.ResourceManager.Api.csproj", "Services.ResourceManager/"]
COPY ["Services.ResourceManager.AccessLayer.Azure/Services.ResourceManager.AccessLayer.Azure.csproj", "Services.ResourceManager.AccessLayer.Azure/"]
COPY ["Services.ResourceManager.AccessLayer/Services.ResourceManager.AccessLayer.csproj", "Services.ResourceManager.AccessLayer/"]
COPY ["Services.ResourceManager.DTO/Services.ResourceManager.DTO.csproj", "Services.ResourceManager.DTO/"]
RUN dotnet restore "Services.ResourceManager/Services.ResourceManager.Api.csproj"
COPY . .
WORKDIR "/src/Services.ResourceManager"
RUN dotnet build "Services.ResourceManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.ResourceManager.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.ResourceManager.Api.dll"]