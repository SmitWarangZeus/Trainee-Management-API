# Re-review: Task1 / Day2 (Review 1)

Hi! I checked your code again against the earlier Day 2 feedback. Nice progress, many items are fixed.

**Overall:** Good job. Most Day 2 feedback is addressed. A few coding-practice items remain.

## What you fixed since last review

- **Validation messages** now match the spec: email says `"Valid email is required"`, status says `"Status is required"`.
- **Validation removed from the `Trainee` entity**: rules live on DTOs only, as Day 2 asked.
- `**UpdatedDate`** is set on every update (`DateTime.UtcNow`).
- `**_service` is private** in `TraineeController`.
- **POST returns 201 Created** using `Created(...)`.
- **Unused imports** removed from `TraineeService`.
- **DTOs reorganized** under `DTOs/Trainee/` for a cleaner structure.
- **Global exception handler** added for consistent API errors, a good step beyond Day 2.
- **Logging** added in the service layer.

## Coding practices: still to do

- **DRY**: `CreateTraineeRequest` and `UpdateTraineeRequest` are still identical. Consider a shared base class or common validation helper. (Good to have. Do it if you have time)
- **Constants / enum**: status values (`"Active"`, `"Inactive"`, `"Completed"`) and error messages are still repeated as strings. Add a `Constants` or `ValidationMessages` class, or a `TraineeStatus` enum.
- **Parameter naming**: still using `Id` instead of `id` in controllers and services.
- **Spacing**: `trainee==null` should be `trainee == null`.
- **Consistent not-found handling**: `GetById` and `Update` throw `NotFoundException`, but `Delete` still returns `false`. Pick one pattern for all endpoints.

## Next step for you

1. Add a small **Constants**
2. Shared DTO base to reduce duplication. (Good to have. Do it if you have time)

