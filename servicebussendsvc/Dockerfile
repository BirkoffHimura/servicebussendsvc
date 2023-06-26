#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8081
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["servicebussendsvc/servicebussendsvc.csproj", "servicebussendsvc/"]
RUN dotnet restore "servicebussendsvc/servicebussendsvc.csproj"
COPY . .
WORKDIR "/src/servicebussendsvc"
RUN dotnet build "servicebussendsvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "servicebussendsvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["/bin/bash", "entrypoint.sh"]