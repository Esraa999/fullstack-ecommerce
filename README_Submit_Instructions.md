# Full Stack Assignment - What to submit & step-by-step

This archive contains:
- `backend_dotnet/` : .NET 7 Web API skeleton with JWT auth, refresh token, EF Core models.
- `frontend_angular/` : Angular skeleton (login + product list) with RxJS state sample.
- `sql/` : SQL scripts and backup placeholders.

## How to open in Visual Studio (backend)
1. Open Visual Studio 2022 or 2023.
2. File -> Open -> Project/Solution -> select `backend_dotnet/ECommerce.sln` (placeholder: create a new solution and add the `ECommerce.API` project if needed).
3. Restore NuGet packages.
4. Update `appsettings.json` connection string if needed.
5. Run `dotnet ef migrations add InitialCreate` then `dotnet ef database update` (or run the provided SQL script).
6. Run the API (F5). Swagger will be available at `/swagger`.

## What to expect from backend
- Controllers: Auth (register/login/refresh), Products (CRUD - protected by JWT).
- Image uploads saved to `UploadedImages/` inside API working directory.
- EF Core with SQL Server. You can use LocalDB for quick testing.

## How to run frontend
1. Install Node 16+ and Angular CLI.
2. `cd frontend_angular`
3. `npm install` (you will need to add necessary Angular packages)
4. `ng serve` and open `http://localhost:4200`
5. Configure proxy or base URL to point to backend (e.g., proxy.conf.json).

## Files to submit
- Full source code (backend + frontend).
- SQL script `sql/create_db.sql` (included below).
- A database backup (.bak) if using SQL Server (create via SSMS or `BACKUP DATABASE`).
- README with instructions (this file).
