FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
ENV HUSKY=0
ENV CI=true

COPY src/microservices/Tasks/Tasks.API/*.csproj src/microservices/Tasks/Tasks.API/
COPY src/microservices/Tasks/Tasks.Application/*.csproj src/microservices/Tasks/Tasks.Application/
COPY src/microservices/Tasks/Tasks.Infrastructure/*.csproj src/microservices/Tasks/Tasks.Infrastructure/
COPY src/microservices/Tasks/Tasks.Domain/*.csproj src/microservices/Tasks/Tasks.Domain/
COPY src/shared-libs/ServiceDefaults/*.csproj src/shared-libs/ServiceDefaults/

RUN dotnet restore src/microservices/Tasks/Tasks.API/Tasks.API.csproj

COPY . .

RUN dotnet publish src/microservices/Tasks/Tasks.API/Tasks.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Tasks.API.dll"]