FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["GoldenCrown.csproj", "."]
RUN dotnet restore "GoldenCrown.csproj"
COPY . .
RUN dotnet publish "GoldenCrown.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GoldenCrown.dll"]