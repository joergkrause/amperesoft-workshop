#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkshopSolution.BackendServiceCtrl/WorkshopSolution.BackendServiceCtrl.csproj", "WorkshopSolution.BackendServiceCtrl/"]
COPY ["WorkshopSolution.BusinessLogic/WorkshopSolution.BusinessLogic.csproj", "WorkshopSolution.BusinessLogic/"]
COPY ["WorkshopSolution.DataTransferObjects/WorkshopSolution.DataTransferObjects.csproj", "WorkshopSolution.DataTransferObjects/"]
COPY ["WorkshopSolution.Persistence/WorkshopSolution.Persistence.csproj", "WorkshopSolution.Persistence/"]
COPY ["WorkshopSolution.Repositories/WorkshopSolution.Repositories.csproj", "WorkshopSolution.Repositories/"]
COPY ["WorkshopSolution.DomainModels/WorkshopSolution.DomainModels.csproj", "WorkshopSolution.DomainModels/"]
RUN dotnet restore "./WorkshopSolution.BackendServiceCtrl/./WorkshopSolution.BackendServiceCtrl.csproj"
COPY . .
WORKDIR "/src/WorkshopSolution.BackendServiceCtrl"
RUN dotnet build "./WorkshopSolution.BackendServiceCtrl.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkshopSolution.BackendServiceCtrl.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkshopSolution.BackendServiceCtrl.dll"]