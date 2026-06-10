# Feedback — Task2 / Day1

Hi! Here is feedback on your **Trainee-Management-API** work for **Task2 / Day1**.

**Overall:** Good job

## What you did well

- You switched from InMemory to **MySQL** using `UseMySQL` in `Program.cs` — well done.
- Connection string is read from `**appsettings.json`** via `GetConnectionString("DefaultConnection")` — not hardcoded in controllers or services.
- `**InitialCreate` migration** exists and creates the `Trainees` table with all expected columns.
- All CRUD APIs are wired through EF Core and should work with MySQL.
- README includes connection string setup and `dotnet ef database update`.
- You committed your work to Git (`49a50b3 Added NoTracking and shifted to SQL DB`).

## What is missing or incomplete

- README does not mention `**dotnet ef migrations add InitialCreate`** — the requirements list both migration commands; add the create step for someone setting up fresh.
- **Swagger/Postman evidence** is required — add screenshots if not submitted yet.
- Test the **restart check**: create a trainee, restart the API, confirm the record still exists — good proof that MySQL persistence works.

## Coding practices to improve

- **Do not commit real passwords** — `appsettings.json` has `Password=root`. For learning, use **User Secrets** (`dotnet user-secrets`) or `appsettings.Development.json` (gitignored) for local passwords. Keep a placeholder in the committed file.
- **Use a constant for the connection string name** — `"DefaultConnection"` appears in code; a `public const string DefaultConnection = "DefaultConnection"` avoids typos if referenced in multiple places.
- **Align database name with the spec** — requirements suggest `trainee_management_db`; yours uses `Traineedb`. Either is fine if documented, but matching the spec reduces confusion.
- **Update `UpdatedDate` on PUT** — still not refreshed in `TraineeService.UpdateAsync` when editing a trainee.
- **Return 201 Created on POST** — still returning `200 OK` when creating a resource.
- **Make `_service` private** in `TraineeController` — same fix from earlier days.
- **README typo** — `Trainee-Managemnt-API` should be `Trainee-Management-API`.
- **Document the full MySQL setup** — mention installing MySQL, creating the database, running both EF commands, then `dotnet run`.

## Next step for you

- Update README with full migration steps (`migrations add` + `database update`) and MySQL setup notes.
- Move the real password out of committed `appsettings.json` (use User Secrets or a dev-only config file).
- Run the create → restart → verify test and capture Swagger/Postman screenshots.

