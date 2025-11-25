# Survey-Basket

A fully functional, production-ready Survey & Basket REST API scaffold built with ASP.NET Core, featuring modular layered architecture (Api, Application, Domain, Infrastructure). This README is a template cloned in structure and style from another reference project — fill the sections below with real project details, diagrams, and screenshots as you complete them.

---

## Tech Stack

| Layer         | Technology                                  |
|---------------|---------------------------------------------|
| Language      | C#                                          |
| Framework     | ASP.NET Core (Web API)                      |
| ORM           | Entity Framework Core                       |
| Security      | JWT (ASP.NET Core Authentication)           |
| Token Store   | (optional) Redis / In-memory cache          |
| DB            | SQL Server / SQLite / PostgreSQL            |
| Testing       | xUnit / MSTest / Postman                    |
| Build Tool    | dotnet CLI                                  |
| Docs          | Swagger / Swashbuckle                       |
| Validation    | Data Annotations / FluentValidation         |

---

## Database Design

### ERD
(Place ERD image here when available)

### DB Schemas
(Place DB schema screenshots here)

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
├── Application/             # Application layer: services, DTOs, interfaces, use-cases
├── Domain/                  # Domain models, entities, enums, value objects
├── Infrastructure/          # EF Core DbContext, repositories, Migrations, data access
└── README.md
```

---

## Strength Points

- **Layered (Clean) Architecture** for separation of concerns and testability.
- **Feature-based organization** making it easy to locate controllers, services, and repositories.
- **DTO-based API layer** for clear request/response contracts.
- **Centralized configuration** via appsettings.json + environment configs.
- **Swagger/OpenAPI** ready for documentation and testing (if enabled).
- **EF Core migrations** supported in Infrastructure (if configured).
- **Extensible middleware pipeline** for cross-cutting concerns (logging, exception handling, auth).

---

## Security Features

- **JWT Authentication & Authorization**
- **Role-based access control** (Admin / User) — implement policies as needed
- **Environment-based secrets** (use Secret Manager or env vars in production)
- **HTTPS recommended for production**

---

## Roles & Capabilities

| Role   | Capabilities                                     |
|--------|--------------------------------------------------|
| User   | Browse surveys, fill surveys, manage personal basket |
| Admin  | Create/update/delete surveys, manage questions, view all responses |

---

## Controllers & EndPoints

(Replace the placeholders below with actual controllers and routes from Api/Controllers)

- AuthController
  - POST /api/auth/register
  - POST /api/auth/login
- SurveysController
  - GET /api/surveys
  - GET /api/surveys/{id}
  - POST /api/surveys
  - PUT /api/surveys/{id}
  - DELETE /api/surveys/{id}
- QuestionsController
  - POST /api/surveys/{surveyId}/questions
  - PUT /api/surveys/{surveyId}/questions/{questionId}
  - DELETE /api/surveys/{surveyId}/questions/{questionId}
- ResponsesController
  - POST /api/surveys/{surveyId}/responses
  - GET /api/surveys/{surveyId}/responses
- BasketController
  - GET /api/basket
  - POST /api/basket/items
  - DELETE /api/basket/items/{itemId}
  - POST /api/basket/confirm

(Use Api/Api.http to add real example requests and then paste them here)

---

## Installation & Run

```bash
# Clone the repository
git clone https://github.com/Mostafa-Ibrahim-294/Survey-Basket.git

# Navigate into the project
cd Survey-Basket

# Restore and run (API project)
dotnet run --project Api
```

Configuration
- Edit Api/appsettings.json or Api/appsettings.Development.json to set:
  - ConnectionStrings: DefaultConnection
  - Jwt: Key, Issuer, Audience, ExpiresInMinutes
- For EF Core migrations (if using EF):
  - Create migration:
    dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api
  - Apply migrations:
    dotnet ef database update --project Infrastructure --startup-project Api

---

## Example Requests

Login (curl):
```bash
curl -X POST "http://localhost:5000/api/auth/login" -H "Content-Type: application/json" -d '{
  "email": "user@example.com",
  "password": "P@ssw0rd"
}'
```

Submit survey response:
```bash
curl -X POST "http://localhost:5000/api/surveys/1/responses" \
  -H "Authorization: Bearer <JWT_TOKEN>" \
  -H "Content-Type: application/json" \
  -d '{
    "respondentId": "user-123",
    "answers": [
      { "questionId": 10, "answerText": "Free text answer" },
      { "questionId": 11, "selectedOptionIds": [21] }
    ]
  }'
```

---

## Testing

- If test projects are added, run:
```bash
dotnet test
```
- Use Postman / REST Client to run Api/Api.http requests.

---

## Deployment

- Build and publish:
```bash
dotnet publish Api -c Release -o ./publish
```
- Consider containerizing with Docker (add Dockerfile and docker-compose if desired).
- Configure production secrets via environment variables or a secrets manager.

---

## Contributing

1. Fork the repo and create a feature branch:
   git checkout -b feature/my-feature
2. Implement changes with clear commits.
3. Open a Pull Request describing your changes and tests.

---

## License

Add a LICENSE file to the repository root (e.g., MIT) if you want to open-source the project.

---

## Contact

- Repository owner: Mostafa-Ibrahim-294

(Replace the placeholders and add real diagrams, real endpoints from Api/Controllers, real tech versions and any screenshots. Fill the template with concrete details from your codebase where noted.)
