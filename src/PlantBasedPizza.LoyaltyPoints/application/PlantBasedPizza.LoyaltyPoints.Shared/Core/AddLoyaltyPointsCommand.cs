namespace PlantBasedPizza.LoyaltyPoints.Shared.Core;

public class AddLoyaltyPointsCommand
{
    public required string CustomerIdentifier { get; set; }

    public required string OrderIdentifier { get; set; }

    public decimal OrderValue { get; set; }

    public bool Validate()
    {
        return !string.IsNullOrEmpty(OrderIdentifier) && !string.IsNullOrEmpty(CustomerIdentifier);
    }
}