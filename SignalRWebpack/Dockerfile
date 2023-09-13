FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SignalRWebpack.csproj", "./"]
RUN dotnet restore "SignalRWebpack.csproj"

COPY . .
# WORKDIR "/src/SignalRWebpack"
RUN dotnet build "SignalRWebpack.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRWebpack.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherService.dll"]