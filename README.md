# Blog Engine (.NET 9 + Blazor Web)

A complete **Blog Engine** built with **.NET 9** and **Blazor Web Server**, including:

* **Blog API (Backend)**  manages blogs and authors
* **Blazor App (Frontend)** Admin Panel and Client Site combined
* **Blog.Shared** shared models and utilities used by both API and Blazor app

The application is fully responsive and works on **mobile, tablet, and desktop**.

---

## Project Structure

* `BlogAPI` ASP.NET Core 9 Web API
* `BlazorApp` Blazor Web Server app (Admin + Client)
* `Blog.Shared` shared models and DTOs

All projects are in the same solution and reference `Blog.Shared` for consistency.

---

## Setup

### 1. Clone the repository

### 2. Configure Database

* Open `BlogAPI/appsettings.json`
* Set your connection string (SQL Server). Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BlogDb;Trusted_Connection=True;"
}
```

### 3. Generate HTTPS Development Certificate (if needed)

```bash
dotnet dev-certs https --trust
```

> This creates and trusts a self-signed development certificate for HTTPS.

---

### 4. Apply Migrations & Update Database in BlogAPI

```bash
dotnet ef database update 
```

> EF Core reads the database name from the connection string.

---

### 5. Run the Projects

**Blog API (Backend)** runs on `https://localhost:7177`

```bash
cd BlogAPI
dotnet watch run
```

**Blazor App (Frontend)** HTTPS port displayed on run

```bash
cd BlogApp
dotnet run
```

> The Blazor App contains both Admin Panel and Client Site.

---
## Quich start
| Step | Project / Action           | Command / URL                                                                     | Notes                                          |
| ---- | -------------------------- | --------------------------------------------------------------------------------- | ---------------------------------------------- |
| 1    | Clone repository           |  Navigate into project folder after clone       |
| 2    | Configure database         | Edit `BlogAPI/appsettings.json`                                                  | Set connection string (SQLite or SQL Server)   |
| 3    | Generate HTTPS certificate | `dotnet dev-certs https --trust`                                                  | Needed if no local cert exists                 |
| 4    | Apply migrations in API    | Database name is taken from connection string  |
| 5    | Apply migrations manually  | `dotnet ef database update --project Blog.Api`                                    | EF Core reads DB name from connection string   |
| 6    | Run API                    | `cd BlogAPI` <br> `dotnet run`                                                   | Runs on `https://localhost:7177`               |
| 7    | Run Blog App             | `cd BlogApp` <br> `dotnet run`                                                  | Port displayed on run (on 7012) |
| 8    | Open in browser            | `https://localhost:7177` (API) <br> `https://localhost:7012` (Blog App)         | Admin Panel & Client Site are in the same app  |


## Notes

* Ensure **EF Core tools** are installed:

```bash
dotnet tool install --global dotnet-ef
```

* Shared models in `Blog.Shared` are referenced by both API and Blazor app.
* All migrations are included; database updates automatically using the name from the connection string.
* Rich text editor or markdown support in Blazor App requires a compatible Blazor component (e.g., `Blazored.TextEditor`).
* Frontend is fully responsive for mobile, tablet, and desktop.

---
