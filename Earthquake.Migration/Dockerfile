FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Earthquake.Migration/Earthquake.Migration.csproj", "Earthquake.Migration/"]
RUN dotnet restore "Earthquake.Migration/Earthquake.Migration.csproj"
COPY . .
WORKDIR "/src/Earthquake.Migration"
RUN dotnet build "Earthquake.Migration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Earthquake.Migration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Earthquake.Migration.dll"]
