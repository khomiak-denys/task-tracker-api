FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
ENV HUSKY=0
ENV CI=true

COPY src/microservices/Users/Users.API/*.csproj src/microservices/Users/Users.API/
COPY src/microservices/Users/Users.Application/*.csproj src/microservices/Users/Users.Application/
COPY src/microservices/Users/Users.Infrastructure/*.csproj src/microservices/Users/Users.Infrastructure/
COPY src/microservices/Users/Users.Domain/*.csproj src/microservices/Users/Users.Domain/
COPY src/shared-libs/ServiceDefaults/*.csproj src/shared-libs/ServiceDefaults/

RUN dotnet restore src/microservices/Users/Users.API/Users.API.csproj

COPY . .

RUN dotnet publish src/microservices/Users/Users.API/Users.API.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Users.API.dll"]