cd src/PlantBasedPizza.Account && docker buildx build --load -f application/PlantBasedPizza.Account.Api/Dockerfile-x86 -t local.account-api ../
cd ../../
cd src/PlantBasedPizza.Delivery && docker buildx build --load -f application/PlantBasedPizza.Delivery.Api/Dockerfile-x86 -t local.delivery-api ../ && docker buildx build --load -f application/PlantBasedPizza.Delivery.Worker/Dockerfile-x86 -t local.delivery-worker ../
cd ../../
cd src/PlantBasedPizza.Kitchen && docker buildx build --load -f application/PlantBasedPizza.Kitchen.Api/Dockerfile-x86 -t local.kitchen-api ../ && docker buildx build --load -f application/PlantBasedPizza.Kitchen.Worker/Dockerfile-x86 -t local.kitchen-worker ../
cd ../../
cd src/PlantBasedPizza.LoyaltyPoints && docker buildx build --load -f application/PlantBasedPizza.LoyaltyPoints.Api/Dockerfile-x86 -t local.loyalty-api ../ && docker buildx build --load -f application/PlantBasedPizza.LoyaltyPoints.Worker/Dockerfile-x86 -t local.loyalty-worker ../ && docker buildx build --load -f application/PlantBasedPizza.LoyaltyPoints.Internal/Dockerfile-x86 -t local.loyalty-internal-api ../
cd ../../
cd src/PlantBasedPizza.Orders && docker buildx build --load -f application/PlantBasedPizza.Orders.Worker/Dockerfile-x86 -t local.order-worker ../ && docker buildx build --load -f application/PlantBasedPizza.Orders.Api/Dockerfile-x86 -t local.order-api ../ && docker buildx build --load -f application/PlantBasedPizza.Orders.Internal/Dockerfile-x86 -t local.order-internal ../
cd ../../
cd src/PlantBasedPizza.Payments && docker buildx build --load -f application/PlantBasedPizza.Payments/Dockerfile-x86 -t local.payment-api ../
cd ../../
cd src/PlantBasedPizza.Recipes && docker buildx build --load -f applications/PlantBasedPizza.Recipes.Api/Dockerfile-x86 -t local.recipe-api ../
cd ../../
cd src/frontend && docker buildx build --load -t frontend .