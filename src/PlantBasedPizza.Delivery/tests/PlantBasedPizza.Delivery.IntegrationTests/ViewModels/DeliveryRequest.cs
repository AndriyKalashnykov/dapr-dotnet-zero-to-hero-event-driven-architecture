using System.Text.Json.Serialization;

namespace PlantBasedPizza.Delivery.IntegrationTests.ViewModels
{
    public class DeliveryRequest
    {
        [JsonPropertyName("orderIdentifier")]
        public required string OrderIdentifier { get; set; }

        [JsonPropertyName("driver")]
        public required string Driver { get; set; }

        [JsonPropertyName("awaitingCollection")]
        public bool AwaitingCollection { get; set; }

        [JsonPropertyName("deliveryAddress")]
        public required Address DeliveryAddress { get; set; }

        [JsonPropertyName("driverCollectedOn")]
        public DateTime? DriverCollectedOn { get; set; }

        [JsonPropertyName("deliveredOn")]
        public DateTime? DeliveredOn { get; set; }
    }
}