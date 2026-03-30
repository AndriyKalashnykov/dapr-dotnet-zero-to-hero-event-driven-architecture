[![CI](https://github.com/AndriyKalashnykov/dapr-dotnet-zero-to-hero-event-driven-architecture/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/AndriyKalashnykov/dapr-dotnet-zero-to-hero-event-driven-architecture/actions/workflows/ci.yml)
[![Hits](https://hits.sh/github.com/AndriyKalashnykov/dapr-dotnet-zero-to-hero-event-driven-architecture.svg?view=today-total&style=plastic)](https://hits.sh/github.com/AndriyKalashnykov/dapr-dotnet-zero-to-hero-event-driven-architecture/)
[![License: MIT](https://img.shields.io/badge/License-MIT-brightgreen.svg)](https://opensource.org/licenses/MIT)
[![Renovate enabled](https://img.shields.io/badge/renovate-enabled-brightgreen.svg)](https://app.renovatebot.com/dashboard#github/AndriyKalashnykov/dapr-dotnet-zero-to-hero-event-driven-architecture)

# Plant Based Pizza

A C#/.NET 10 microservices application demonstrating event-driven architecture using [Dapr](https://dapr.io/). Based on the [Dometrain Zero to Hero Event-Driven Architecture](https://github.com/Dometrain/zero-to-hero-event-driven-architecture/tree/main/module6) course. Services include Account, Delivery, Kitchen, Loyalty Points, Orders, Payments, and Recipes.

## Quick Start

```bash
make restore        # restore NuGet packages
make build          # build the solution
make test           # run unit tests
make image-build    # build Docker images for all services
make run-local      # start all services via Docker Compose
```

## Prerequisites

| Tool | Version | Purpose |
|------|---------|---------|
| [.NET SDK](https://dotnet.microsoft.com/download) | 10.0+ | Build and run the application |
| [Docker](https://www.docker.com/) | latest | Container builds and local infrastructure |
| [GNU Make](https://www.gnu.org/software/make/) | 3.81+ | Build orchestration |
| [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/) | latest | Run individual services locally (optional) |

Install all required dependencies:

```bash
make deps
```

## Available Make Targets

Run `make help` to see all available targets.

### Build & Run

| Target | Description |
|--------|-------------|
| `make restore` | Restore NuGet packages |
| `make build` | Build the solution |
| `make test` | Run unit tests |
| `make lint` | Check code formatting |
| `make format` | Auto-fix code formatting |
| `make clean` | Clean build artifacts |
| `make run-local` | Start all services locally via Docker Compose |

### Docker

| Target | Description |
|--------|-------------|
| `make image-build` | Build Docker images for all services |
| `make image-build-arm` | Build Docker images (ARM) for all services |

### CI

| Target | Description |
|--------|-------------|
| `make ci` | Full CI pipeline: restore, lint, test, build |
| `make ci-run` | Run GitHub Actions workflow locally via [act](https://github.com/nektos/act) |

### Utilities

| Target | Description |
|--------|-------------|
| `make help` | List available tasks |
| `make deps` | Install and verify required dependencies |
| `make deps-prune` | Detect unused and redundant dependencies |
| `make renovate-validate` | Validate Renovate configuration |
| `make release` | Create and push a new tag |

## Running Individual Services

All microservices can run independently from their respective folder under [src](./src/):

1. Start infrastructure: `docker-compose up -d`
2. Start the API and Dapr sidecar from the service directory (two terminals):
    - `cd src/PlantBasedPizza.<Service> && make local-api`
    - `cd src/PlantBasedPizza.<Service> && make dapr-api-sidecar`
3. If the service has a worker component (two more terminals):
    - `cd src/PlantBasedPizza.<Service> && make local-worker`
    - `cd src/PlantBasedPizza.<Service> && make dapr-worker-sidecar`

Admin interface: [http://localhost:3000/admin/login](http://localhost:3000/admin/login) — default credentials: `admin@plantbasedpizza.com` / `AdminAccount!23`

## CI/CD

GitHub Actions runs on every push to `main`, tags `v*`, and pull requests.

| Job | Triggers | Steps |
|-----|----------|-------|
| **ci** | push, PR, tags | Restore, Lint, Test, Build |
| **cleanup** | weekly (Sunday) | Delete old workflow runs (retain 7 days, keep minimum 5 runs) |

[Renovate](https://docs.renovatebot.com/) keeps dependencies up to date with platform automerge enabled.

## References

* [Original](https://github.com/Dometrain/zero-to-hero-event-driven-architecture/tree/main/module6)
