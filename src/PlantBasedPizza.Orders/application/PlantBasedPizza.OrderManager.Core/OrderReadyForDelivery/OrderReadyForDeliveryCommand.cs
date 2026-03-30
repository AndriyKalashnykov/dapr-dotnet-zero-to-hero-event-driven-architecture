namespace PlantBasedPizza.OrderManager.Core.OrderReadyForDelivery;

public record OrderReadyForDeliveryCommand
{
    public required string OrderIdentifier { get; set; }
}