FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SensorPasivo/SensorPasivo.csproj", "SensorPasivo/"]
RUN dotnet restore "SensorPasivo/SensorPasivo.csproj"
COPY . .
WORKDIR "/src/SensorPasivo"
RUN dotnet build "SensorPasivo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorPasivo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensorPasivo.dll"]
