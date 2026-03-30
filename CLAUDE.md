# Plant Based Pizza

C#/.NET 9 microservices application using Dapr for event-driven architecture.

## Project Structure

- `PlantBasedPizza.sln` — root solution file
- `global.json` — pins .NET SDK version
- `src/` — service source code (Account, Delivery, Kitchen, LoyaltyPoints, Orders, Payments, Recipes, frontend)
- `src/shared/` — shared libraries (Events, Shared, IntegrationTest.Helpers)

## Build

```bash
make restore    # restore NuGet packages
make build      # dotnet build the solution
make test       # dotnet test the solution
make lint       # dotnet format --verify-no-changes
make ci         # full local CI pipeline
```

## CI/CD

- `.github/workflows/ci.yml` — CI pipeline: restore, lint, build, test
- `.github/workflows/cleanup-runs.yml` — weekly cleanup of old workflow runs

## Skills

Use the following skills when working on related files:

| File(s) | Skill |
|---------|-------|
| `Makefile` | `/makefile` |
| `renovate.json` | `/renovate` |
| `README.md` | `/readme` |
| `.github/workflows/*.yml` | `/ci-workflow` |

When spawning subagents, always pass conventions from the respective skill into the agent's prompt.
