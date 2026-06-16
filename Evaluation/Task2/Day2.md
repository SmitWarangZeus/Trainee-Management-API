# Feedback: Task2 / Day2

Hi! Here is feedback on your **Trainee-Management-API** work for **Task2 / Day2**.

## What you did well

- **Users table** is created through EF Core migration with Id, Username, Email, PasswordHash, Role, CreatedDate, UpdatedDate.
- **Password hashing** uses BCrypt. Plain text passwords are never stored in the database.
- **PasswordHash is not returned** in `UserResponse` or `LoginResponse`.
- **Login API** works and returns a JWT token on success.
- **JWT settings** come from `appsettings.json` (`JwtSettings`), not hardcoded in controller code.
- **JwtService** reads issuer, audience, expiry, and key from config and builds a signed token.
- Auth DTOs exist: `LoginRequest`, `LoginResponse`, `UserResponse`.
- Login logging does not write passwords or hashes to the log.

## What is missing or incomplete

- **Login URL**: requirements say `POST /api/auth/login`. Because the action uses `[HttpPost("/login")]` (leading `/`), the actual route is `**POST /login`**, not `/api/auth/login`.
- **No seeded Admin user**: requirements ask to seed or create at least one Admin for testing (`admin` / `Admin@123`). There is a `/register` endpoint, but no migration seed and no documented default admin. RIght now its hardcoded right?
- **Wrong status on unknown username**: missing user throws `NotFoundException` (404). Login failures should return **401 Unauthorized**, not 404.
- `**ExpiresIn` is hardcoded** to `3600` in `LoginResponse` instead of using `JwtSettings:ExpiryMinutes` (60 minutes = 3600 seconds is fine, but it should come from config).
- **Username uniqueness**: requirements say username should be unique, but there is no unique constraint or validation on duplicate usernames.

## Coding practices to improve

- **Move JWT key out of committed config**: `JwtSettings:Key` and DB password are in `appsettings.json`. Use User Secrets or environment variables for local dev.
- **Register hardcodes email and role**: `Email = "admin@email.com"` and `Role = "admin"` for every user. Use a proper registration DTO with email and role from input.
- **AuthController**: make `_service` `private readonly`, not `public readonly`.
- **JWT claims**: token uses standard claim names (`Sub`, `UniqueName`, `role`). Requirements mention UserId, Username, Role. Works, but custom claim names like `userId` can make frontend code clearer.
- **Put `JwtService` in a namespace** (e.g. `TraineeManagement.api.Services`) to match the rest of the project.
- **Config section name**: requirements show `"Jwt"` but project uses `"JwtSettings"`. Fine if documented, but note the difference.

## Next step for you

1. Change `[HttpPost("/login")]` to `[HttpPost("login")]` and test that Swagger shows `**POST /api/auth/login`**.
2. Return **401** for all failed logins (wrong username or password).
3. Seed or document a test Admin user and read `ExpiresIn` from JWT config.

