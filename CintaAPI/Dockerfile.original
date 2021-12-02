FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Cinta.API/Cinta.API.csproj", "Cinta.API/"]
RUN dotnet restore "Cinta.API/Cinta.API.csproj"
COPY . .
WORKDIR "/src/Cinta.API"
RUN dotnet build "Cinta.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cinta.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cinta.API.dll"]
