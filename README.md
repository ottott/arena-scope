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

*(Coming soon)*

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
cd Arena.Api
dotnet run
```

## Run the frontend

```bash
cd arena-frontend
npm install
npm run dev
```