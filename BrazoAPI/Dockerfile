FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BrazoAPI/BrazoAPI.csproj", "BrazoAPI/"]
RUN dotnet restore "BrazoAPI/BrazoAPI.csproj"
COPY . .
WORKDIR "/src/BrazoAPI"
RUN dotnet build "BrazoAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrazoAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrazoAPI.dll"]
