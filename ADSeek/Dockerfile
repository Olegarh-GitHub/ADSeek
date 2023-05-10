FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ADSeek/ADSeek.csproj", "ADSeek/"]
RUN dotnet restore "ADSeek/ADSeek.csproj"
COPY . .
WORKDIR "/src/ADSeek"
RUN dotnet build "ADSeek.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ADSeek.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ADSeek.dll"]
