FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy AS base
ENV DOTNET_CLI_TELEMETRY_OPTOUT 1
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY ["src/Api/Messages.Api.csproj", "src/Api/"]
RUN dotnet restore "src/Api/Messages.Api.csproj"
COPY . .

# Build App
WORKDIR /src/src/Api
RUN dotnet build Messages.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Messages.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Messages.Api.dll"]