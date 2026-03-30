using Newtonsoft.Json;

namespace PlantBasedPizza.Delivery.IntegrationTests.ViewModels
{
    public class AssignDriverRequest
    {
        [JsonProperty("orderIdentifier")]
        public required string OrderIdentifier { get; set; }

        [JsonProperty("driverName")]
        public required string DriverName { get; set; }
    }
}