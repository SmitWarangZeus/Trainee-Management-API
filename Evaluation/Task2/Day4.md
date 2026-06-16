# Feedback: Task2 / Day4

Hi! Here is feedback on your **Trainee-Management-API** work for **Task2 / Day4**.

## What you did well

- **Mentor module** is complete: model, DTOs, validation, service, controller, and CRUD endpoints.
- **Learning Task module** is complete with the required fields and CRUD APIs.
- APIs are **protected with JWT** using `[Authorize]` on both controllers.
- **Migrations** exist for Mentors and LearningTasks tables.
- You follow the same pattern as Trainee: thin controllers, service layer, DTOs, and `Created` on POST.
- Pagination and search on list endpoints is a nice extra.

## What is missing or incomplete

- **Learning Task PUT** does not update `DueDate` in `LearningTaskService.UpdateAsync`, even though the update DTO includes it.
- **Created URL typo** on learning task POST: `api/learningTasks/...` should match the route `api/learning-tasks/...`.

## Coding practices to improve

- **Fix filename typo**: `UpdateMentorRequst.cs` should be `UpdateMentorRequest.cs`.
- **Use interfaces for services**: `IMentorService` and `ILearningTaskService` are good. Keep the same pattern for newer modules.
- **Consistent not-found handling**: Delete returns `false` + 404, but Get/Update throw `NotFoundException`. Pick one approach.
- **Email search**: mentor search still uses case-sensitive `Contains` on email (same issue as trainee module).
- **Validation message typo**: Learning Task description says `"Max length 50 characters"` but limit is 200.
- **Constants / enums**: status values like `Active`, `Draft`, `Published` are still string literals. Consider a shared constants or enum approach.
- **Parameter naming**: use `id` instead of `Id` in method signatures.

## Next step for you

1. Fix **the coding practices**

