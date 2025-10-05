# E-Commerce Fullstack Application README

This repository contains a fullstack e-commerce application built as an assignment skeleton. It features a modern Angular frontend integrated with a .NET backend API, using JWT authentication, SQL Server database, and responsive UI with Angular Material. The app supports user registration/login, protected product listing (with discounts and images), and secure API calls.

The project is divided into two main parts:
- **Frontend**: Angular 18+ single-page application (SPA) for user interface and client-side logic.
- **Backend**: .NET 8+ Web API for authentication, data management, and business logic.

Below are separate README sections for each. Clone the repo and follow the setup for both to run the full application.

---

## Frontend README (Angular E-Commerce Client)

### Project Overview
The frontend is a standalone Angular application (no NgModules) that provides a responsive e-commerce interface. Users can register/login with JWT tokens, view protected product listings (grid with cards, discounted prices, images), and handle errors gracefully. It uses reactive forms, RxJS for state management, HTTP interceptors for auth, and Angular Material for UI.

**Key Features**:
- User authentication (login/register) with token storage in localStorage.
- Route guards to protect `/products` page.
- Automatic token refresh on 401 errors.
- Product grid with loading spinner, error handling, and retry functionality.
- Responsive design (mobile-friendly cards and toolbar).

### Technologies Used
- **Framework**: Angular 18+ (standalone components, signals optional).
- **UI Library**: Angular Material 18+ (toolbar, cards, buttons, forms, grid-list, spinner).
- **State Management**: RxJS (BehaviorSubject for auth state).
- **Forms**: Reactive Forms Module.
- **HTTP**: Angular HttpClient with interceptors (auth headers, error handling).
- **Routing**: Angular Router with guards (AuthGuard).
- **Testing**: Jasmine/Karma (unit tests), Cypress (E2E).
- **Build Tool**: Angular CLI.
- **Other**: TypeScript, SCSS, RxJS operators (tap, catchError, switchMap).

### Prerequisites
- Node.js 18+ (with npm 9+).
- Angular CLI 18+ (`npm install -g @angular/cli@18`).
- A running backend API (see Backend README) on `https://localhost:61321`.
- SQL Server (e.g., SQL Server Express or LocalDB) for backend DB.

### Installation and Setup
1. **Clone/Navigate**:
   ```bash
   git clone <repo-url> ecommerce-frontend  # Or use the skeleton
   cd ecommerce-frontend
   ```

2. **Install Dependencies**:
   ```bash
   npm install
   ```

3. **Add Angular Material** (if not pre-installed):
   ```bash
   ng add @angular/material
   ```
   - Choose: Global typography (Yes), Theme (Indigo/Pink), Animations (Yes).

4. **Environment Configuration**:
   - Edit `src/environments/environment.ts` (dev) and `environment.prod.ts` (prod):
     ```typescript
     export const environment = {
       production: false,  // true for prod
       apiUrl: 'https://localhost:61321'  // Backend URL (update port if needed)
     };
     ```

5. **Run the Application**:
   ```bash
   ng serve --open
   ```
   - Opens `http://localhost:4200`.
   - Register a user → Login → View products (requires backend data).

6. **Build for Production**:
   ```bash
   ng build --configuration production
   ```
   - Outputs to `dist/ecommerce-frontend/` (optimized bundle).

### Features and Usage
- **Login/Register**: Form validation (required fields, email pattern). Switches between modes.
- **Products Page**: Loads via API (protected). Displays grid of cards:
  - Product name, category, code, image (or placeholder), discounted price (e.g., $100 - 10% = $90), min quantity.
  - Loading spinner during fetch; error message with "Retry" button if failed.
- **Auth Flow**: Tokens auto-added to headers; refresh on expiry; logout clears storage.
- **Error Handling**: User-friendly messages (e.g., "Failed to load products"); console logs for debug.
- **Routing**:
  - `/` → Login (default).
  - `/products` → Protected list (redirects unauth users).
  - `/register` → Registration form.

### Testing
- **Unit Tests** (Components/Services):
  ```bash
  ng test
  ```
  - Covers auth service (login mock), product component (loading state), guard (route protection).
  - Coverage: Aim for 80%+ (run `ng test --code-coverage`).

- **E2E Tests** (Cypress):
  ```bash
  npm install --save-dev cypress  # If not installed
  npx cypress open
  ```
  - Tests: Register → Login → View products → Logout.
  - Specs in `cypress/e2e/` (e.g., auth.cy.ts, products.cy.ts).

### Troubleshooting
- **Compilation Errors**: Run `ng cache clean` and `npm install`. Ensure Material imports (e.g., `MatCardModule`).
- **API 500/401 Errors**: Check backend running (port 61321), CORS enabled, DB seeded with products.
- **CORS Issues**: Backend must allow `http://localhost:4200`.
- **HTTPS Cert**: If "NET::ERR_CERT", run `dotnet dev-certs https --trust` (backend side).
- **No Products**: Seed DB with sample data (see Backend README).
- **Logs**: Use browser F12 (Console/Network) for Angular errors; backend console for API issues.

### Deployment
- **Frontend**: Host `dist/` on Netlify/Vercel (static SPA). Update `apiUrl` to prod backend (e.g., Azure/Heroku).
- **Integrate with Backend**: Use environment vars for API URL.
- **Security**: Tokens in localStorage (fine for SPA; use HttpOnly cookies for prod).

### License
MIT License. For educational/assignment use. Contact for modifications.

---

## Backend README (.NET E-Commerce API)

### Project Overview
The backend is a secure Web API built with .NET 8+ and Entity Framework Core. It handles user authentication (JWT with refresh tokens), product management, and database operations. Endpoints are protected with authorization, and it integrates with SQL Server for data persistence. Swagger UI provides API documentation and testing.

**Key Features**:
- JWT-based authentication (login/register with hashed passwords, refresh tokens).
- Protected endpoints (e.g., products require valid token).
- CRUD for products (focus on GET for listing).
- CORS enabled for Angular frontend.
- Error handling with detailed logging.
- Database migrations and seeding support.

### Technologies Used
- **Framework**: .NET 8+ (ASP.NET Core Web API).
- **ORM**: Entity Framework Core 8+ (SQL Server provider).
- **Auth**: JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer), BCrypt for password hashing.
- **Database**: SQL Server (LocalDB/Express), with DbContext for Users/Products.
- **API Docs**: Swashbuckle (Swagger/OpenAPI).
- **Other**: AutoMapper (optional for DTOs), Serilog/Console logging, CORS middleware.
- **Testing**: xUnit/NUnit (unit/integration), EF In-Memory for mocks.

### Prerequisites
- .NET 8+ SDK (`dotnet --version` to check).
- SQL Server (e.g., SQL Server Management Studio for management; LocalDB for dev).
- Visual Studio 2022+ or VS Code with C# extension (optional).
- Frontend running on `http://localhost:4200` for full testing.

### Installation and Setup
1. **Clone/Navigate**:
   ```bash
   git clone <repo-url> ecommerce-backend  # Or use the skeleton (ECommerce.API)
   cd backend_dotnet/ECommerce.API
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   dotnet build  # Check for errors (warnings like JWT version are minor)
   ```

3. **Database Setup**:
   - **Connection String**: Edit `appsettings.json` (or `appsettings.Development.json`):
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true;"
       },
       "Jwt": {
         "Key": "YourSuperSecretKeyHereAtLeast32CharsLong",  // Generate a strong key
         "Issuer": "ECommerceAPI",
         "Audience": "ECommerceClient",
         "ExpiryMinutes": 60  // Access token expiry
       }
     }
     ```
     - Customize: Server (e.g., `localhost` or `.\SQLEXPRESS`), Database (`ECommerceDb`).
     - For SQL auth: Add `User   Id=sa;Password=yourpass;`.

   - **Create Database**:
     - Run `create_db.sql` script in SSMS (creates `ECommerceDb` with Users/Products tables).
     - Or use EF migrations (code-first):
       ```bash
       dotnet tool install --global dotnet-ef
       dotnet ef migrations add InitialCreate
       dotnet ef database update
       ```

   - **Seed Data** (Users auto-created on register; add products via SQL):
     ```sql
     INSERT INTO Products (Category, ProductCode, Name, ImagePath, Price, MinimumQuantity, DiscountRate)
     VALUES ('Electronics', 'LAP001', 'Gaming Laptop', 'UploadedImages/laptop.jpg', 1299.99, 1, 15.00);
     -- Add more as needed
     ```

4. **Run the API**:
   ```bash
   dotnet run
   ```
   - Listens on `https://localhost:61321` (HTTPS) and `http://localhost:61322` (HTTP fallback).
   - Open `https://localhost:61321/swagger` for API docs.

5. **Trust Dev Cert** (First Time):
   ```bash
   dotnet dev-certs https --trust
   ```

### Features and API Endpoints
- **Authentication** (No auth required):
  - `POST /api/auth/register` (Body: `LoginDto` {username, password, email}) → Creates user, returns `User  Dto`.
  - `POST /api/auth/login` (Body: `LoginDto` {username, password}) → Returns `AuthResultDto` {success, accessToken, refreshToken}.
  - `POST /api/auth/refresh` (Body: {refreshToken}) → New tokens if valid.

- **Products** (Requires JWT in `Authorization: Bearer <token>` header):
  - `GET /api/products` → Returns array of `ProductDto` (id, category, productCode, name, imagePath, price, minimumQuantity, discountRate).
  - `POST /api/products` (Optional: Body `CreateProductDto`) → Adds product (admin-only in prod).

- **Security**:
  - Passwords hashed with BCrypt.
  - Tokens: Access (short-lived), Refresh (longer, stored in DB with expiry).
  - Protected routes use `[Authorize]` attribute.

- **Swagger Testing**:
  - Open `/swagger` → Authorize (Bearer token from login) → Test endpoints.

### Testing
- **Unit/Integration Tests**:
  ```bash
  dotnet test
  ```
  - Covers controllers (e.g., AuthController login mock), services (JWT validation), DbContext (In-Memory DB).

- **API Testing**:
  - Use Swagger or Postman: Login → Copy token → GET /api/products.
  - Integration: Run frontend + backend → Full user flow.

### Troubleshooting
- **500 on /api/products**: Ensure Products table exists/seeded (see DB Setup). Check console for EF errors (e.g., "Invalid object name 'Products'").
- **Auth Exception** ("No authenticationScheme"): Verify `Program.cs` has:
  ```csharp
  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => { /* JWT config */ });
  app.UseAuthentication();
  app.UseAuthorization();
  ```
- **CORS Errors**: In `Program.cs`:
  ```csharp
  builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy =>
      policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
  });
  app.UseCors("AllowAngular");  // Before UseAuthentication
  ```
- **JWT Warnings**: Update `System.IdentityModel.Tokens.Jwt` to latest (`dotnet add package System.IdentityModel.Tokens.Jwt --version 7.0.3`); ignore moderate vuln for dev.
- **DB Connection**: Test in SSMS: `SELECT * FROM Users;`. Update connection string if "Cannot open database".
- **Logs**: Set `"LogLevel": { "Default": "Debug" }` in appsettings for detailed output.

### Deployment
- **Backend**: Publish to Azure App Service/IIS (`dotnet publish`). Use Azure SQL for prod DB. Set JWT key in env vars.
- **HTTPS**: Enforce in prod (`app.UseHttpsRedirection();`).
- **Integrate with Frontend**: Update Angular `apiUrl` to prod backend URL.
- **Security**: Use secrets manager for JWT key; enable HTTPS-only.

### License
MIT License. For educational/assignment use. Ensure DB scripts and models match your skeleton.

---

For questions or issues, refer to the troubleshooting sections or share logs/errors. To run the full app: Start backend → Seed DB → Run frontend → Register/Login → View products. Enjoy! 🚀
