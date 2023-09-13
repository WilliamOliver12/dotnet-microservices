FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IceCreamStore.csproj", "./"]
RUN dotnet restore "IceCreamStore.csproj"

COPY . .
# WORKDIR "/src/IceCreamStore"
RUN dotnet build "IceCreamStore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IceCreamStore.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IceCreamStore.dll"]