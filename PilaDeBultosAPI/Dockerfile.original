FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PilaDeBultosAPI/PilaDeBultosAPI.csproj", "PilaDeBultosAPI/"]
RUN dotnet restore "PilaDeBultosAPI/PilaDeBultosAPI.csproj"
COPY . .
WORKDIR "/src/PilaDeBultosAPI"
RUN dotnet build "PilaDeBultosAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PilaDeBultosAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PilaDeBultosAPI.dll"]
