FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
ENV HUSKY=0
ENV CI=true

COPY src/microservices/Workspaces/Workspaces.API/*.csproj src/microservices/Workspaces/Workspaces.API/
COPY src/microservices/Workspaces/Workspaces.Application/*.csproj src/microservices/Workspaces/Workspaces.Application/
COPY src/microservices/Workspaces/Workspaces.Infrastructure/*.csproj src/microservices/Workspaces/Workspaces.Infrastructure/
COPY src/microservices/Workspaces/Workspaces.Domain/*.csproj src/microservices/Workspaces/Workspaces.Domain/
COPY src/shared-libs/ServiceDefaults/*.csproj src/shared-libs/ServiceDefaults/

RUN dotnet restore src/microservices/Workspaces/Workspaces.API/Workspaces.API.csproj

COPY . .

RUN dotnet publish src/microservices/Workspaces/Workspaces.API/Workspaces.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Workspaces.API.dll"]
