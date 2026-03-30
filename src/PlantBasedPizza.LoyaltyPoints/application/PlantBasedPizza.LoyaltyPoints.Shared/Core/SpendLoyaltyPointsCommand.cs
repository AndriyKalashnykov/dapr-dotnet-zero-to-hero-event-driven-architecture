namespace PlantBasedPizza.LoyaltyPoints.Shared.Core;

public class SpendLoyaltyPointsCommand
{
    public required string CustomerIdentifier { get; set; }

    public required string OrderIdentifier { get; set; }

    public decimal PointsToSpend { get; set; }
}