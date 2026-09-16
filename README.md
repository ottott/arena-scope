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

- Linux with Git and curl. On Ubuntu: `sudo apt update && sudo apt install -y git curl ca-certificates`.
- **.NET 8 SDK** (including the ASP.NET Core 8 runtime). Follow the [.NET Ubuntu installation guide](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu). On Ubuntu 24.04: `sudo apt install -y dotnet-sdk-8.0`.
- **Node.js 22.12+ (22.x) or 24 LTS**, with npm. Install a supported version from [Node.js](https://nodejs.org/en/download); older Ubuntu `nodejs` packages may be too old for Vite.
- **Docker Engine and the Docker Compose plugin**, with the daemon running. Follow the [Ubuntu Docker installation instructions](https://docs.docker.com/engine/install/ubuntu/), which include the Compose plugin. Docker Desktop is not required. The commands below use `sudo docker`; omit `sudo` if your user already has Docker access.
- A valid **Riot Games API key** from the [Riot Developer Portal](https://developer.riotgames.com/). Renew expired development keys before analyzing a player.

Verify the tools before continuing:

```bash
dotnet --list-sdks            # Must include 8.0.x
dotnet --list-runtimes       # Must include Microsoft.AspNetCore.App 8.0.x
node --version              # 22.12+ (22.x), or 24.x
npm --version
sudo docker compose version
sudo docker info            # Confirms the daemon is reachable
```

Ports **5432**, **5271**, and **5173** must be available. Internet access is needed to restore dependencies and download Riot/Data Dragon/Community Dragon data. The backend currently uses Riot's **Europe** routing endpoint.

## Fresh-clone setup

Run these commands in a Bash terminal. All commands in this section run from the repository root:

```bash
git clone https://github.com/ottott/arena-scope.git
cd arena-scope

cp frontend/.env.example frontend/.env

dotnet restore backend/Arena.sln
dotnet user-secrets set "Riot:ApiKey" "YOUR_API_KEY" --project backend/Arena.Api

sudo docker compose up -d --wait

# Match the Entity Framework tool to the backend's EF Core version.
dotnet tool install --tool-path ./.tools dotnet-ef --version 8.0.11
./.tools/dotnet-ef database update --project backend/Arena.Api

(cd frontend && npm ci)
```

Replace `YOUR_API_KEY` with your key. The project already has a `UserSecretsId`, so `dotnet user-secrets init` is unnecessary. The key is stored outside the repository and is loaded by the Development launch profile.

Compose runs **PostgreSQL only** and creates its persistent volume automatically. Database migrations are required before the first analysis; rerun the `database update` command after pulling new migrations. If `.tools/dotnet-ef` is already installed, skip the tool installation command.

The local database defaults in `backend/Arena.Api/appsettings.json` match Compose: host `localhost`, port `5432`, database/user/password `arena`. These are local development credentials.

`frontend/.env` sets `VITE_API_URL=http://localhost:5271/api`. Include `/api` when changing the URL. Vite reads it at startup/build time, so restart the dev server or rebuild after changes. All `VITE_` values are public browser configuration; keep the Riot API key in backend user secrets.

## Run locally

In one terminal, from the repository root:

```bash
dotnet run --project backend/Arena.Api --launch-profile http
```

The API listens at **http://localhost:5271**; Swagger is at **http://localhost:5271/swagger**. The explicit HTTP profile avoids needing a trusted HTTPS development certificate on Linux.

In a second terminal, from the repository root:

```bash
cd frontend
npm run dev -- --host localhost --port 5173 --strictPort
```

Open **http://localhost:5173**. Enter the Riot ID and tag in separate fields, then select **Analyze**. The first sync can take time while matches are imported. The backend's CORS policy allows this exact frontend origin; keep port 5173 and use `localhost` rather than `127.0.0.1`.

Stop the app servers with Ctrl+C. Stop PostgreSQL from the repository root with:

```bash
sudo docker compose down
```

Database data persists between runs. `sudo docker compose down --volumes` deletes the local database; run migrations again if you deliberately reset it.

## Build

After the setup steps, run from the repository root:

```bash
dotnet build backend/Arena.sln
(cd frontend && npm run build)
```

The frontend build runs TypeScript checks and writes production assets to `frontend/dist`. Neither build requires PostgreSQL or a Riot API key; running the application does.

## Troubleshooting

- **Database connection fails:** run `sudo docker compose ps` and `sudo docker compose logs postgres`, check port 5432, and ensure the migration command succeeded.
- **Analysis fails:** check the Riot ID/tag, confirm the backend is running, and update an expired API key using the user-secrets command. Restart the backend after changing the key.
- **Frontend cannot reach the API:** check `frontend/.env`, restart Vite, and use the exact frontend/backend URLs above.
- **Backend fails during startup:** it downloads item and augment metadata; check access to Data Dragon and Community Dragon.
