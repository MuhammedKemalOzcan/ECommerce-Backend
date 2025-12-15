FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/ECommerceAPI.API/ECommerceAPI.API.csproj", "Presentation/ECommerceAPI.API/"]
COPY ["Core/ECommerceAPI.Application/ECommerceAPI.Application.csproj", "Core/ECommerceAPI.Application/"]
COPY ["Core/ECommerceAPI.Domain/ECommerceAPI.Domain.csproj", "Core/ECommerceAPI.Domain/"]
COPY ["Infrastructure/ECommerceAPI.Infrastructure/ECommerceAPI.Infrastructure.csproj", "Infrastructure/ECommerceAPI.Infrastructure/"]
COPY ["Infrastructure/ECommerceAPI.Persistence/ECommerceAPI.Persistence.csproj", "Infrastructure/ECommerceAPI.Persistence/"]
RUN dotnet restore "./Presentation/ECommerceAPI.API/ECommerceAPI.API.csproj"
COPY . .
WORKDIR "/src/Presentation/ECommerceAPI.API"
RUN dotnet build "./ECommerceAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ECommerceAPI.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceAPI.API.dll"]