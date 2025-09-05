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

### 2. Configure Database (SQL LocalDB)
Open BlogAPI/appsettings.json

### 2a. Create and Start SQL LocalDB Instance
### Create a new LocalDB instance 
sqllocaldb create BlogLocalDB

#### Start the instance
sqllocaldb start BlogLocalDB

#### Optional: check status
sqllocaldb info BlogLocalDB

### 2b. Apply EF Core Migrations
### Create a new migration (BlogAPI dir)
dotnet ef migrations add InitialCreate

### Update the database to apply migrations
dotnet ef database update 

EF Core will create the database on the running LocalDB instance specified in your connection string.

### 3. Generate HTTPS Development Certificate (if needed)

```bash
dotnet dev-certs https --trust
```

> This creates and trusts a self-signed development certificate for HTTPS.

---


### 4. Run the Projects (where .csproj)

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


## Notes

* Ensure **EF Core tools** are installed:

```bash
dotnet tool install --global dotnet-ef
```
