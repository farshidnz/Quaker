FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Earthquake.Consumers/Earthquake.Consumers.csproj", "Earthquake.Consumers/"]
RUN dotnet restore "Earthquake.Consumers/Earthquake.Consumers.csproj"
COPY . .
WORKDIR "/src/Earthquake.Consumers"
RUN dotnet build "Earthquake.Consumers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Earthquake.Consumers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Earthquake.Consumers.dll"]
