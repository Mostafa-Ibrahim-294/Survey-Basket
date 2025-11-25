# Survey-Basket

A fully functional Survey Basket REST API scaffold built with ASP.NET Core, featuring clean architecture (Api, Application, Domain, Infrastructure).Uses Entity Framework Core for persistence, and secures endpoints with JWT-based authentication. It includes production-focused tooling such as Serilog request logging, health checks, rate limiting, CORS, Hangfire background jobs, and seeders to populate initial data. Swagger/OpenAPI documentation and Api.http samples are provided to speed up development and integration with clients.

---

## Project Structure

```bash
Survey-Basket/
├── .gitattributes
├── .gitignore
├── SurveyBasket.sln
├── Api/
│   ├── Api.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Api.http
│   ├── Controllers/         # REST Controllers (API endpoints)
│   ├── Extensions/          # Service registration, swagger, auth extensions
│   ├── Middlewares/         # Custom middleware components
│   └── Properties/
├── Application/             # Application layer: contracts, DTOs, interfaces, use-cases
├── Domain/                  # Domain models, entities, errors
├── Infrastructure/          # EF Core DbContext, repositories, Migrations, data access
└── README.md
```

## Tech Stack

| Layer         | Technology                                  |
|---------------|---------------------------------------------|
| Language      | C#                                          |
| Framework     | .Net 9                                      |
| ORM           | EF Core                                     |
| Security      | JWT (ASP.NET Identity)                      |
| Token Store   | (Hybrid) Redis / In-memory cache            |
| DB            | SQL Server                                  |
| Testing       | Postman                                     |
| Build Tool    | dotnet CLI                                  |
| Docs          | Swagger / Swashbuckle                       |
| Validation    | Data Annotations / FluentValidation         |

---

## Database Design

### DB Schemas

![WhatsApp Image 2025-11-25 at 04 10 17_dafc605a](https://github.com/user-attachments/assets/4bd180ff-21a9-4d74-979a-4f60b5867e52)

---


---

## Strength Points

- **Clean Architecture** for separation of concerns and testability.
- **CQRS Pattern** making it easy to locate controllers, Commands, Queries and repositories.
- **Mediator Pattern** Decoupled Query/command handling (MediatR-style) to centralize business logic, simplify handlers, and improve testability.
- **DTO-based API layer** for clear request/response contracts.
- **Centralized configuration** via appsettings.json + environment configs.
- **Logging** Structured request and error logging with Serilog with contextual enrichers.
- **Erorrs And Exception Handling** Using Result Pattern and Global Exception Middleware For handling issues gracefullly
- **Caching** Support for Hybrid caching to reduce DB load and improve performance.
- **Background Jobs** Background processing with Hangfire for scheduled, recurring, and fire-and-forget jobs.
- **Rate Limiting** Request throttling middleware to protect endpoints, prevent abuse, and maintain service stability under load.
- **Health Check** Health check endpoint that reports application and dependency status for monitoring and orchestration.
- **Pagination** Apply pagination to handle very large result sets.

