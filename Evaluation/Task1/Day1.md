# Feedback — Task1 / Day1

Hi! Here is feedback on your **Trainee-Management-API** work for **Task1 / Day1**.

**Overall:** On track

## What you did well

- Your API project runs and Swagger is set up — good start.
- Health check (`GET /api/health`) works and returns the right fields.
- You created the `Trainee` model with all the required fields.
- `GET /api/trainees`, `GET /api/trainees/{id}`, and `POST /api/trainees` are all working.
- You committed your code to Git.

## What is missing or incomplete

- Day 1 asked for a simple **in-memory list** (`List<Trainee>`). Your current code uses a database instead — fine if you did that later, but be ready to explain the Day 1 version.
- Project name should be `TraineeManagement.Api` — yours uses lowercase `api`.

## Coding practices to improve

- **Use constants for repeated text** — values like `"running"`, `"Trainee Management API"`, and status strings (`"Active"`, `"Inactive"`) are hardcoded in multiple places. Create a `Constants.cs` or `TraineeStatuses.cs` class so you change them in one spot.
- **Keep route paths consistent** — you use full paths like `"/api/health"` and `"/api/trainees"`. A common pattern is `[Route("api/[controller]")]` so routes stay consistent as the project grows.
- **Use `private` for injected dependencies** — `_service` in `TraineeController` should be `private readonly`, not `public readonly`. Other classes should not access your controller's fields.
- **Follow C# naming for parameters** — use `id` (camelCase) instead of `Id` for method parameters. PascalCase is for class and property names.
- **Pick one time style and stick to it** — `HealthController` uses `DateTime.UtcNow` but `Trainee` uses `DateTime.Now`. Use UTC everywhere for APIs unless you have a reason not to.
- **Return the right HTTP status** — use **201 Created** when POST adds a new trainee, not **200 OK**.
- **Avoid copy-paste mistakes** — the `Status` field error says `"Name is required"`. Double-check messages after copying validation attributes.
- **Fix README typo** — `Trainee-Managemnt-API` should be `Trainee-Management-API`.

## Next step for you

- Fix the quick wins: private field, 201 on POST, validation message, README typo.
- Add a simple constants class for status values and health-check strings — good habit to start early.
- Be ready to walk through `Program.cs`, `appsettings.json`, and your controllers without needing later-day code to explain Day 1.

