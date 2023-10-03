#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5555

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TianLiUpdate.API/TianLiUpdate.API.csproj", "TianLiUpdate.API/"]
RUN dotnet restore "TianLiUpdate.API/TianLiUpdate.API.csproj"
COPY . .
WORKDIR "/src/TianLiUpdate.API"
RUN dotnet build "TianLiUpdate.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TianLiUpdate.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TianLiUpdate.API.dll"]