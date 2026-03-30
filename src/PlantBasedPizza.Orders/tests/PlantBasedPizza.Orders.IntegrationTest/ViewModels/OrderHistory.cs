using System.Text.Json.Serialization;

namespace PlantBasedPizza.Orders.IntegrationTest.ViewModels
{
    public class OrderHistory
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("historyDate")]
        public DateTime HistoryDate { get; set; }
    }
}