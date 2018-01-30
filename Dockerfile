FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY "pharmacy.api/pharmacy.api.csproj" ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
WORKDIR pharmacy.api 
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/pharmacy.api/out .
ENTRYPOINT ["dotnet", "pharmacyapi.dll"]