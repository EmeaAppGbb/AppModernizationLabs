# syntax=docker/dockerfile:1

# ── Runtime base ──────────────────────────────────────────────
# TODO: Update to mcr.microsoft.com/dotnet/aspnet:10.0 when .NET 10 is GA
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# ── Build stage ───────────────────────────────────────────────
# TODO: Update to mcr.microsoft.com/dotnet/sdk:10.0 when .NET 10 is GA
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY src/Dashboard/ .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ── Final image ───────────────────────────────────────────────
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Dashboard.dll"]
