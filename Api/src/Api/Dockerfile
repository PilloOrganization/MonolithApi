FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Api/src/Api.sln", "Api/src/"]
COPY ["Api/src/Api/Api.csproj", "Api/src/Api/"]
COPY ["Api/src/Application/Application.csproj", "Api/src/Application/"]
COPY ["Api/src/Infrastructure/Infrastructure.csproj", "Api/src/Infrastructure/"]
COPY ["Api/src/Domain/Domain.csproj", "Api/src/Domain/"]

RUN dotnet restore "Api/src/Api.sln"

COPY . .
WORKDIR "/src/Api/src"
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

EXPOSE 7000
ENV ASPNETCORE_URLS=https://+:7000

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
