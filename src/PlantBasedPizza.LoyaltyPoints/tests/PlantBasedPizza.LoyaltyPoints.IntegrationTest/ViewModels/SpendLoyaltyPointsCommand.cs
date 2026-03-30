namespace PlantBasedPizza.LoyaltyPoints.IntegrationTest.ViewModels;

public class SpendLoyaltyPointsCommand
{
    public required string CustomerIdentifier { get; set; }

    public required string OrderIdentifier { get; set; }

    public decimal PointsToSpend { get; set; }
}