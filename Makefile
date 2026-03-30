.DEFAULT_GOAL := help

APP_NAME       := plant-based-pizza
CURRENTTAG     := $(shell git describe --tags --abbrev=0 2>/dev/null || echo "dev")

# === Tool Versions (pinned) ===
ACT_VERSION    := 0.2.86
NVM_VERSION    := 0.40.4

#help: @ List available tasks
help:
	@echo "Usage: make COMMAND"
	@echo "Commands :"
	@grep -E '[a-zA-Z\.\-]+:.*?@ .*$$' $(MAKEFILE_LIST)| tr -d '#' | awk 'BEGIN {FS = ":.*?@ "}; {printf "\033[32m%-30s\033[0m - %s\n", $$1, $$2}'

#deps: @ Install and verify required dependencies
deps:
	@command -v dotnet >/dev/null 2>&1 || { echo "Error: .NET SDK required. Install from https://dotnet.microsoft.com/download"; exit 1; }
	@echo "All dependencies OK."

deps-act: #deps-act: @ Install act for local CI
	@command -v act >/dev/null 2>&1 || { echo "Installing act $(ACT_VERSION)..."; \
		curl -sSfL https://raw.githubusercontent.com/nektos/act/master/install.sh | sudo bash -s -- -b /usr/local/bin v$(ACT_VERSION); \
	}

#renovate-bootstrap: @ Install nvm and npm for Renovate
renovate-bootstrap:
	@command -v node >/dev/null 2>&1 || { \
		echo "Installing nvm $(NVM_VERSION)..."; \
		curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v$(NVM_VERSION)/install.sh | bash; \
		export NVM_DIR="$$HOME/.nvm"; \
		[ -s "$$NVM_DIR/nvm.sh" ] && . "$$NVM_DIR/nvm.sh"; \
		nvm install --lts; \
	}

#renovate-validate: @ Validate Renovate configuration
renovate-validate: renovate-bootstrap
	@npx --yes renovate-config-validator

#clean: @ Clean build artifacts
clean:
	@dotnet clean PlantBasedPizza.sln -c Release --verbosity quiet

#restore: @ Restore NuGet packages
restore: deps
	@dotnet restore PlantBasedPizza.sln

#build: @ Build the solution
build: deps
	@dotnet build PlantBasedPizza.sln -c Release --no-restore

#test: @ Run unit tests
test: deps
	@for proj in $$(find src -name '*UnitTest*.csproj'); do \
		echo "Testing $$proj..."; \
		dotnet run --project "$$proj" -c Release || exit 1; \
	done

#lint: @ Check code formatting
lint: deps
	@dotnet format PlantBasedPizza.sln --verify-no-changes --verbosity quiet

#ci: @ Run full local CI pipeline (restore, lint, build, test)
ci: restore lint build test
	@echo "Local CI pipeline passed."

#ci-run: @ Run GitHub Actions workflow locally using act
ci-run: deps-act
	@act push --container-architecture linux/amd64 \
		--artifact-server-path /tmp/act-artifacts

#release: @ Create and push a new tag
release:
	@bash -c 'read -p "New tag (current: $(CURRENTTAG)): " newtag && \
		echo "$$newtag" | grep -qE "^v[0-9]+\.[0-9]+\.[0-9]+$$" || { echo "Error: Tag must match vN.N.N"; exit 1; } && \
		echo -n "Create and push $$newtag? [y/N] " && read ans && [ "$${ans:-N}" = y ] && \
		git add -A && \
		git commit -a -s -m "Cut $$newtag release" && \
		git tag $$newtag && \
		git push origin $$newtag && \
		git push && \
		echo "Done."'

# === Docker Builds (delegate to sub-Makefiles) ===

#image-build: @ Build Docker images for all services
image-build: image-build-account image-build-delivery image-build-kitchen image-build-loyalty-points image-build-orders image-build-payments image-build-recipes image-build-frontend

image-build-account:
	@cd src/PlantBasedPizza.Account && make build
image-build-delivery:
	@cd src/PlantBasedPizza.Delivery && make build
image-build-kitchen:
	@cd src/PlantBasedPizza.Kitchen && make build
image-build-loyalty-points:
	@cd src/PlantBasedPizza.LoyaltyPoints && make build
image-build-orders:
	@cd src/PlantBasedPizza.Orders && make build
image-build-payments:
	@cd src/PlantBasedPizza.Payments && make build
image-build-recipes:
	@cd src/PlantBasedPizza.Recipes && make build
image-build-frontend:
	@cd src/frontend && docker build -t frontend .

#image-build-arm: @ Build Docker images (ARM) for all services
image-build-arm: image-build-account-arm image-build-delivery-arm image-build-kitchen-arm image-build-loyalty-points-arm image-build-orders-arm image-build-payments-arm image-build-recipes-arm image-build-frontend

image-build-account-arm:
	@cd src/PlantBasedPizza.Account && make build-arm
image-build-delivery-arm:
	@cd src/PlantBasedPizza.Delivery && make build-arm
image-build-kitchen-arm:
	@cd src/PlantBasedPizza.Kitchen && make build-arm
image-build-loyalty-points-arm:
	@cd src/PlantBasedPizza.LoyaltyPoints && make build-arm
image-build-orders-arm:
	@cd src/PlantBasedPizza.Orders && make build-arm
image-build-payments-arm:
	@cd src/PlantBasedPizza.Payments && make build-arm
image-build-recipes-arm:
	@cd src/PlantBasedPizza.Recipes && make build-arm

#run-local: @ Start all services locally via Docker Compose
run-local:
	@docker compose up -d && sleep 10 && docker compose -f docker-compose-services.yml up -d

.PHONY: help deps deps-act renovate-bootstrap renovate-validate clean restore build test lint ci ci-run release \
	image-build image-build-account image-build-delivery image-build-kitchen image-build-loyalty-points \
	image-build-orders image-build-payments image-build-recipes image-build-frontend \
	image-build-arm image-build-account-arm image-build-delivery-arm image-build-kitchen-arm \
	image-build-loyalty-points-arm image-build-orders-arm image-build-payments-arm image-build-recipes-arm \
	run-local
