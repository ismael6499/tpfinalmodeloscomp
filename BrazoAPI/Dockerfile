FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Brazo.API/Brazo.API.csproj", "Brazo.API/"]
RUN dotnet restore "Brazo.API/Brazo.API.csproj"
COPY . .
WORKDIR "/src/Brazo.API"
RUN dotnet build "Brazo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Brazo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brazo.API.dll"]
