namespace PlantBasedPizza.Orders.IntegrationTest.ViewModels
{
    public class OrderItem
    {
        public string RecipeIdentifier { get; set; } = string.Empty;

        public string ItemName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}