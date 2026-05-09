FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["GoldenCrown.TaxInspectionConsole2/*.csproj", "./GoldenCrown.TaxInspectionConsole2/"]
RUN dotnet restore "./GoldenCrown.TaxInspectionConsole2/GoldenCrown.TaxInspectionConsole2.csproj"
COPY . .
RUN dotnet build "./GoldenCrown.TaxInspectionConsole2/GoldenCrown.TaxInspectionConsole2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./GoldenCrown.TaxInspectionConsole2/GoldenCrown.TaxInspectionConsole2.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoldenCrown.TaxInspectionConsole2.dll"]