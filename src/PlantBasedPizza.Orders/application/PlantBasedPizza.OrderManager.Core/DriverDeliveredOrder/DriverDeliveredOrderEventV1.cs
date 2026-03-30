using PlantBasedPizza.Events;

namespace PlantBasedPizza.OrderManager.Core.DriverDeliveredOrder;

public class DriverDeliveredOrderEventV1 : IntegrationEvent
{
    public override string EventName => "delivery.driverDeliveredOrder";
    public override string EventVersion => "v1";
    public override Uri Source => new Uri("https://delivery.plantbasedpizza");

    public required string OrderIdentifier { get; init; }
}