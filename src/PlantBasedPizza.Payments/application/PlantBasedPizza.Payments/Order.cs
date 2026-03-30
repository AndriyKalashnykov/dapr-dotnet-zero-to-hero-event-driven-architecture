namespace PlantBasedPizza.Payments;

public class Order
{
    public required string OrderIdentifier { get; set; }

    public decimal OrderValue { get; set; }
}