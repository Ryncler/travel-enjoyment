#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 54500
ENV TZ=Asia/Shanghai

COPY . .
ENTRYPOINT ["dotnet", "SightsService.Host.dll"]