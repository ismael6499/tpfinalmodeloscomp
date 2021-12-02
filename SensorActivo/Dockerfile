FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sensor.Activo/Sensor.Activo.csproj", "Sensor.Activo/"]
RUN dotnet restore "Sensor.Activo/Sensor.Activo.csproj"
COPY . .
WORKDIR "/src/Sensor.Activo"
RUN dotnet build "Sensor.Activo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sensor.Activo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensor.Activo.dll"]
