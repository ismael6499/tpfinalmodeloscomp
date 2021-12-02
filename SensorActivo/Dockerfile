FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SensorActivo/SensorActivo.csproj", "SensorActivo/"]
RUN dotnet restore "SensorActivo/SensorActivo.csproj"
COPY . .
WORKDIR "/src/SensorActivo"
RUN dotnet build "SensorActivo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorActivo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensorActivo.dll"]
