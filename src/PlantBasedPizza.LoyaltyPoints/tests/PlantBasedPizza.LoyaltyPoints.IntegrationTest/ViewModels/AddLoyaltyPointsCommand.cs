namespace PlantBasedPizza.LoyaltyPoints.IntegrationTest.ViewModels;

public class AddLoyaltyPointsCommand
{
    public required string CustomerIdentifier { get; set; }

    public required string OrderIdentifier { get; set; }

    public decimal OrderValue { get; set; }
}