# Feedback: Task2 / Day3

Hi! Here is feedback on your **Trainee-Management-API** work for **Task2 / Day3**.

**Overall:** Good job

## What you did well

- **Trainee APIs are JWT protected** with `[Authorize]` on `TraineeController`.
- **Health** and **login** stay public (no `[Authorize]` on those controllers).
- **Pagination** works with `pageNumber`, `pageSize`, `Skip()`, and `Take()`.
- **Status filter** works via `PaginationParams.Status`.
- **Paged response** matches the required shape: `pageNumber`, `pageSize`, `totalRecords`, `data`.
- **CORS** is configured for `http://localhost:3000` and `http://localhost:5173`. Not wide open.
- **Structured logging** is in place for login, trainee create/update/delete, and not-found cases.
- Passwords and tokens are not written to logs.

## What is missing or incomplete

- **Search query param name**: requirements use `?search=amit`, but your model binds `SearchTerm` (expects `searchTerm`). Clients following the spec will not hit your search unless they use the wrong param name or you add `[FromQuery(Name = "search")]`.

## Coding practices to improve

- **Fix search binding** so `search` works as in the requirements:
  ```csharp
  [FromQuery(Name = "search")]
  public string SearchTerm { get; set; } = "";
  ```
- **Email search** is still case-sensitive while other fields use `.ToLower()`. Keep behavior consistent.
- **Log login failure** with a generic message (you log "Password incorrect", which is OK). Avoid logging usernames on failure if you want stricter security later.
- **Pagination defaults**: document default `pageSize` (10) in README so testers know what to expect.

## Next step for you

1. Fix the `**search` query parameter** and test: `GET /api/trainees?pageNumber=1&pageSize=10&search=amit&status=Active` with a valid JWT.
2. Test **401** without a token and with an invalid token in Swagger.

