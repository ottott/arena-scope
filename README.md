# Arena Scope

Arena Scope is a web application for analyzing **League of Legends Arena** match history. It synchronizes matches directly from the Riot API, stores them in a local database, and provides detailed statistics on player performance, champions, items, augments, and teammates.

The goal of the project is to provide meaningful Arena-specific insights that are not available in the official League client.

## Features

### Player Analysis

* Analyze any Riot ID (`GameName#TagLine`)
* Automatically synchronize Arena match history
* Season-aware syncing to avoid unnecessary API requests
* Incremental updates after the initial import

### Overall Statistics

* Games played
* Win rate
* Top 3 rate
* Average placement
* Placement distribution chart
* Combat performance (KDA)
* Damage, healing and shielding averages
* Best teammates
* Best team champions
* Champion performance table

### Item Analytics

* Performance statistics for every item used
* Games played
* Average placement
* Top 3 rate
* Win rate
* Sortable table

### Augment Analytics

* Performance statistics for every augment
* Games played
* Average placement
* Top 3 rate
* Win rate
* Sortable table

### Planned Features

* Match history page with detailed match breakdown
* Champion and item icons
* Advanced filtering engine
* Match detail view
* Additional Arena-specific analytics

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
* Axios
* Chart.js

---

## Architecture

The application consists of two main parts:

* **Backend** – Handles Riot API communication, match synchronization, data processing, and exposes a REST API.
* **Frontend** – Displays interactive statistics and analytics through a modern Vue interface.

During synchronization, matches are downloaded once and stored locally, allowing statistics to be generated quickly without repeatedly querying Riot's API.

---

## Screenshots

*(Coming soon)*

---

## Running Locally

### Backend

```bash
cd backend
dotnet restore
dotnet ef database update
dotnet run
```

### Frontend

```bash
cd frontend
npm install
npm run dev
```

The frontend expects the backend API to be running locally.
