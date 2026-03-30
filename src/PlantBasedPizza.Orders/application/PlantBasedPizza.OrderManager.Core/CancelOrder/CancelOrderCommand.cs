using System.Text.Json.Serialization;

namespace PlantBasedPizza.OrderManager.Core.CancelOrder;

public record CancelOrderCommand
{
    [JsonPropertyName("orderIdentifier")]
    public required string OrderIdentifier { get; set; }
}