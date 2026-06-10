# Feedback — Task1 / Day3

Hi! Here is feedback on your **Trainee-Management-API** work for **Task1 / Day3**.

**Overall:** On track

## What you did well

- You removed the manual `List<Trainee>` and now store data through EF Core — exactly what Day 3 was aiming for.
- Your `TraineeContext` is registered in `Program.cs` and wired into the service with DI.
- All CRUD APIs work through the database layer.
- Search works — `GET /api/trainees?search=...` filters across name, email, and tech stack.
- Good use of `AsNoTracking()` on read queries — shows you are thinking about performance.
- README is updated with API list and sample requests.
- You committed your work to Git (including the in-memory DB commit in your history).

## What is missing or incomplete

- Day 3 asked for **EF Core InMemory** (`UseInMemoryDatabase`). Your latest code uses **MySQL** instead. You did add in-memory earlier (`93da057`), but the current version is Task 2 work.
- Day 3 asked for a context named `**AppDbContext`** — yours is `TraineeContext`. It works the same, but the name does not match the requirement.
- README now shows MySQL setup steps, not InMemory — fine for later tasks, but does not reflect Day 3 setup.
- Swagger/Postman testing evidence is mentioned in Day 3 — add screenshots if you have not yet.

## Coding practices to improve

- **Update `UpdatedDate` on every edit** — `TraineeService.UpdateAsync` changes fields but never refreshes `UpdatedDate`. Always set it when data changes.
- **Keep search logic consistent** — email search uses `Contains(search)` without `.ToLower()`, but other fields use `searchLower`. Searches may behave differently for email vs name.
- **Extract search fields or use a shared pattern** — the search `Where` clause repeats similar checks. A small private method or consistent approach keeps it easier to maintain.
- **Use camelCase for parameters** — `Id` should be `id` in service and controller methods (`GetByIdAsync(int id)`).
- **Add spacing for readability** — `trainee==null` → `trainee == null` (standard C# style).
- **Remove unused imports** — `TraineeService.cs` still imports `Microsoft.AspNetCore.Mvc` with no use.
- **Consider `CancellationToken`** on async service methods — ASP.NET can pass it through from controllers.
- **README typo** — `Trainee-Managemnt-API` should be `Trainee-Management-API`.
- **Be ready to explain the switch** — when you moved from InMemory to MySQL, what changed in `Program.cs`? Good skill to practice.

## Next step for you

- Fix `UpdatedDate` on PUT and make email search case-insensitive like the other fields.
- If submitting for Day 3, be ready to demo or show the in-memory version from your earlier commit.
- Add Swagger/Postman screenshots and clean up the README typo.

