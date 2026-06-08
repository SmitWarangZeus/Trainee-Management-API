# Trainee Management API

## Technology Used
- C#
- Dotnet

## How to Run
1. Clone Github Repository
`git clone https://github.com/SmitWarangZeus/Trainee-Management-API`
2. Navigate to folder
`cd Trainee-Managemnt-API`
3. Run Project
`dotnet run`

## API List
|Method|Endpoint|
|:---|:---|
|Get|/api/health|
|Get|/api/trainees|
|Get|/api/trainees/{id}|
|Post|/api/trainees|
|Put|/api/trainees/{id}|
|Delete|/api/trainees/{id}|
|Get|/api/trainees?search={query}|

## Sample Request and Response JSON
`GET` `/api/health`
Sample Request:
```bash
curl -X 'GET' \
  'http://localhost:5175/api/health' \
  -H 'accept: */*'
```
Sample Response:
```json
{
  "status": "running",
  "application": "Trainee Management API",
  "timestamp": "2026-06-08T12:57:36.4071018Z"
}
```
`POST` `/api/trainees`
Sample Request:
```bash
curl -X 'POST' \
  'http://localhost:5175/api/trainees' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Smit",
  "lastName": "Warang",
  "email": "smit@email.com",
  "techStack": "HTML",
  "status": "Active"
}'
```
Sample Response:
```json
{
  "id": 1,
  "firstName": "Smit",
  "lastName": "Warang",
  "email": "smit@email.com",
  "techStack": "HTML",
  "status": "Active",
  "createdDate": "2026-06-08T18:05:19.9234043+05:30",
  "updatedDate": "2026-06-08T18:05:19.9235406+05:30"
}
```
`GET` `/api/trainees/{id}`
Sample Request:
```bash
curl -X 'GET' \
  'http://localhost:5175/api/trainees/1' \
  -H 'accept: */*'
```
Sample Response:
```json
{
  "id": 1,
  "firstName": "Smit",
  "lastName": "Warang",
  "email": "smit@email.com",
  "techStack": "HTML",
  "status": "Active",
  "createdDate": "2026-06-08T18:05:19.9234043+05:30",
  "updatedDate": "2026-06-08T18:05:19.9235406+05:30"
}
```
`GET` `/api/trainees`
Sample Request:
```bash
curl -X 'GET' \
  'http://localhost:5175/api/trainees' \
  -H 'accept: */*'
```
Sample Response:
```json
[
  {
    "id": 1,
    "firstName": "Smit",
    "lastName": "Warang",
    "email": "smit@email.com",
    "techStack": "HTML",
    "status": "Active",
    "createdDate": "2026-06-08T18:05:19.9234043+05:30",
    "updatedDate": "2026-06-08T18:05:19.9235406+05:30"
  }
]
```
`PUT` `/api/trainees/{id}`
Sample Request:
```bash
curl -X 'PUT' \
  'http://localhost:5175/api/trainees/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "firstName": "Smit",
  "lastName": "Warang",
  "email": "smit@email.com",
  "techStack": "CSS",
  "status": "Active"
}'
```
Sample Response:
```json
{
  "id": 1,
  "firstName": "Smit",
  "lastName": "Warang",
  "email": "smit@email.com",
  "techStack": "CSS",
  "status": "Active",
  "createdDate": "2026-06-08T18:05:19.9234043+05:30",
  "updatedDate": "2026-06-08T18:05:19.9235406+05:30"
}
```
`GET` `/api/trainees?search={query}`
Sample Request:
```bash
curl -X 'GET' \
  'http://localhost:5175/api/trainees?search=Smit' \
  -H 'accept: */*'
```
Sample Response:
```json
[
  {
    "id": 1,
    "firstName": "Smit",
    "lastName": "Warang",
    "email": "smit@email.com",
    "techStack": "CSS",
    "status": "Active",
    "createdDate": "2026-06-08T18:05:19.9234043+05:30",
    "updatedDate": "2026-06-08T18:05:19.9235406+05:30"
  }
]
```
`DELETE` `/api/trainees/{id}`
Sample Request:
```bash
curl -X 'DELETE' \
  'http://localhost:5175/api/trainees/1' \
  -H 'accept: */*'
```

## Known Limitations
- InMemory DB (Not persistent)
- No authentication