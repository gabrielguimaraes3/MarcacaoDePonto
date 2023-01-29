#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarcacaoDePonto/MarcacaoDePonto.csproj", "MarcacaoDePonto/"]
RUN dotnet restore "MarcacaoDePonto/MarcacaoDePonto.csproj"
COPY . .
WORKDIR "/src/MarcacaoDePonto"
RUN dotnet build "MarcacaoDePonto.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarcacaoDePonto.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarcacaoDePonto.dll"]