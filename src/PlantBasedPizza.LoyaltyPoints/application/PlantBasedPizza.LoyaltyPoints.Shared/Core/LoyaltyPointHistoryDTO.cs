namespace PlantBasedPizza.LoyaltyPoints.Shared.Core;

public class LoyaltyPointHistoryDto
{
    public DateTime DateTime { get; set; }

    public string OrderIdentifier { get; set; } = string.Empty;

    public decimal OrderValue { get; set; }

    public decimal PointsAdded { get; set; }
}