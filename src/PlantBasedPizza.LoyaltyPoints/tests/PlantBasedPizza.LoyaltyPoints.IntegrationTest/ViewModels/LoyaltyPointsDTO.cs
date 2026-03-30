using System.Text.Json.Serialization;

namespace PlantBasedPizza.LoyaltyPoints.IntegrationTest.ViewModels;

public class LoyaltyPointsDto
{
    [JsonPropertyName("customerIdentifier")]
    public required string CustomerIdentifier { get; set; }

    [JsonPropertyName("totalPoints")]
    public decimal TotalPoints { get; set; }
}