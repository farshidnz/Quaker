FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Earthquake.Worker/Earthquake.Worker.csproj", "Earthquake.Worker/"]
RUN dotnet restore "Earthquake.Worker/Earthquake.Worker.csproj"
COPY . .
WORKDIR "/src/Earthquake.Worker"
RUN dotnet build "Earthquake.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Earthquake.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Earthquake.Worker.dll"]
