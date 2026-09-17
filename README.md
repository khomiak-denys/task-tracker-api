# TaskTracker

A RESTful API built with ASP.NET Core and Clean Architecture (API → Application → Domain ← Infrastructure).

## Prerequisites

- .NET 10.0 SDK
- PostgreSQL (or your database of choice)

## Quickstart

```powershell
dotnet restore
dotnet build
```

## Tests

```powershell
dotnet test
```

## Project Structure

- `src/` — application source code (4 layers)
- `tests/` — unit & architecture tests

## Architecture

| Layer | Responsibility |
|---|---|
| **Domain** | Entities, value objects, domain events, repository interfaces |
| **Application** | Use cases, commands/queries (MediatR), validators |
| **Infrastructure** | Database, external services, repository implementations |
| **API** | Controllers/endpoints, middleware, DI composition root |

## License

MIT — see [LICENSE](LICENSE).
