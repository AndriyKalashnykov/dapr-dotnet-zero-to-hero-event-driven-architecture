using PlantBasedPizza.Events;

namespace PlantBasedPizza.Kitchen.IntegrationTests.ViewModels;

public class OrderSubmittedEventV1 : IntegrationEvent
{
    public override string EventName => "order.orderSubmitted";
    public override string EventVersion => "v1";
    public override Uri Source => new Uri("https://kitchen.test.plantbasedpizza");

    public required string OrderIdentifier { get; init; }

    public required List<OrderSubmittedEventItem> Items { get; init; }
}

public record OrderSubmittedEventItem
{
    public string ItemName { get; init; } = "";
    public string RecipeIdentifier { get; init; } = "";
}