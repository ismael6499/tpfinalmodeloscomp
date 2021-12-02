FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["PrensaAPI/PrensaAPI.csproj", "PrensaAPI/"]
RUN dotnet restore "PrensaAPI/PrensaAPI.csproj"
COPY . .
WORKDIR "/src/PrensaAPI"
RUN dotnet build "PrensaAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PrensaAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrensaAPI.dll"]
