using PlantBasedPizza.Events;

namespace PlantBasedPizza.LoyaltyPoints.IntegrationTest.ViewModels;

public class OrderCompletedIntegrationEventV1 : IntegrationEvent
{
    public override string EventName => "order.orderCompleted";
    public override string EventVersion => "v1";
    public override Uri Source => new Uri("https://loyaltytest.orders.plantbasedpizza");

    public required string OrderIdentifier { get; set; }

    public required string CustomerIdentifier { get; set; }

    public decimal OrderValue { get; set; }
}