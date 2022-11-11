FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ServiceDesk/ServiceDesk.csproj", "ServiceDesk/"]
RUN dotnet restore "ServiceDesk/ServiceDesk.csproj"
COPY . .
WORKDIR "/src/ServiceDesk"
RUN dotnet build "ServiceDesk.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceDesk.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceDesk.dll"]
