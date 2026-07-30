# Arena Scope

Arena Scope is a web application for analyzing **League of Legends Arena** match history. It synchronizes matches directly from the Riot API, stores them in a local database, and provides detailed statistics on player performance, champions, items, augments, and teammates.

The goal of the project is to provide meaningful Arena-specific insights that are not available in the official League client.

---

## Tech Stack

### Backend

* ASP.NET Core
* Entity Framework Core
* PostgreSQL
* Riot Games API

### Frontend

* Vue 3
* TypeScript
* Vuetify

---

## Screenshots
<img width="869" height="922" alt="Screenshot 2026-07-30 124643" src="https://github.com/user-attachments/assets/c407bd07-83e0-4183-8fed-30378a514424" />

<img width="871" height="845" alt="Screenshot 2026-07-30 125255" src="https://github.com/user-attachments/assets/926c1426-855e-48b0-8da3-dbf272ea8962" />

<img width="866" height="900" alt="Screenshot 2026-07-30 125701" src="https://github.com/user-attachments/assets/5ae4514d-7630-4480-9f76-b887dc0635e4" />


---
## Prerequisites

- .NET SDK
- Node.js
- Docker Desktop

## Configuration

The Riot Games API key is stored using ASP.NET Core User Secrets.

```bash
cd Arena.Api
dotnet user-secrets init
dotnet user-secrets set "Riot:ApiKey" "YOUR_API_KEY"
```

## Start PostgreSQL

```bash
docker compose up -d
```

## Run the backend

```bash
cd backend
cd Arena.Api
dotnet run
```

## Run the frontend

```bash
cd frontend
npm install
npm run dev
```
