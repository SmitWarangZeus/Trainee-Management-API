# Feedback: Task2 / Day5

Hi! Here is feedback on your **Trainee-Management-API** work for **Task2 / Day5**.

## What you did well

- **Task Assignment**, **Submission**, and **Review** modules are in place with POST and GET endpoints.
- **Relationship checks** work: assignment validates trainee, mentor, and learning task exist; submission checks assignment; review checks submission and mentor.
- **Due date rule** is enforced: due date cannot be before assigned date.
- **Global exception handler** is registered and avoids exposing stack traces to clients.
- **JWT auth**, **BCrypt password hashing**, **CORS** restricted to localhost origins, and **EF Core** (no raw SQL) cover several OWASP basics.
- **UserResponse** does not return password hash. Good.
- Migrations exist for TaskAssignments, Submissions, and Reviews tables.

## What is missing or incomplete

- **Unexpected error response shape** does not match the requirement. Day 5 asks for:
  ```json
  { "message": "An unexpected error occurred. Please try again later." }
  ```
  Your handler returns `ProblemDetails` with `title` and `detail: "Unexpected error"` instead.
- **Review `Score`** is marked `[Required]` in the DTO, but the requirements say it is **optional**.

## Coding practices to improve

- **Use interfaces + DI** for `TaskAssignmentService`, `SubmissionService`, and `ReviewService` (like Mentor/Trainee). Controllers depend on concrete classes today. (Good to have)
- **Fix misleading log messages**: several say `"found"` when the record was **not** found (e.g. in `TaskAssignmentService` and `SubmissionService`).
- **Wrong error message**: when learning task is missing on assignment create, the message says `"Task assignment not found"` instead of learning task not found.
- **Inconsistent error style**: due date validation returns `null` + plain `BadRequest("...")` string. Use `BadRequestException` like your other services for a consistent API response.
- **URL casing**: review POST returns `Created("/api/Reviews/...")` but route is `/api/reviews`.
- **AuthController**: `_service` is still `public readonly`. Make it `private readonly`.
- **Secrets in** `appsettings.json`: JWT key and DB password are committed. Move to User Secrets or environment variables for local dev. (Make sure you dont do this on actual projects)
- **Register hardcodes email** to `admin@email.com`. Use input or a proper registration DTO.
- **Score optional**: remove `[Required]` from `Score` if it is truly optional.

## OWASP quick check


| Area                                 | Status     |
| ------------------------------------ | ---------- |
| JWT authentication                   | Done       |
| Protected APIs need token            | Done       |
| Password hashing (BCrypt)            | Done       |
| DTOs, no hash in response            | Done       |
| EF Core, no raw SQL                  | Done       |
| CORS restricted                      | Done       |
| Secrets not in committed config      | Needs work |
| Safe error messages (no stack trace) | Done       |
| Error message format per spec        | Needs work |
| No passwords/tokens in logs          | Done       |


## Next step for you

1. Align **500 error response** with the required `{ "message": "..." }` format.
2. Fix log messages, review URL casing, and make `Score` optional.
3. Move secrets out of committed config, add OWASP checklist notes to README

