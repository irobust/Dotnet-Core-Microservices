FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Weather.Api.csproj", "./"]
RUN dotnet restore "Weather.Api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Weather.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Weather.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Weather.Api.dll"]
