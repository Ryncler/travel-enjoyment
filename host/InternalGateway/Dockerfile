#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 59600

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InternalGateway.csproj", "."]
RUN dotnet restore "./InternalGateway.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "InternalGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InternalGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternalGateway.dll"]