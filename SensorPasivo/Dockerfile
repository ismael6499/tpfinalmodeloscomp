FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Sensor.Pasivo/Sensor.Pasivo.csproj", "Sensor.Pasivo/"]
RUN dotnet restore "Sensor.Pasivo/Sensor.Pasivo.csproj"
COPY . .
WORKDIR "/src/Sensor.Pasivo"
RUN dotnet build "Sensor.Pasivo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sensor.Pasivo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensor.Pasivo.dll"]
