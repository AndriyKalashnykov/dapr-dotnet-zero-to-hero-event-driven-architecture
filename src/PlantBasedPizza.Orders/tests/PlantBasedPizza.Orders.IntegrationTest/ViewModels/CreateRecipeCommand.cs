namespace PlantBasedPizza.Orders.IntegrationTest.ViewModels
{
    public class CreateRecipeCommand
    {
        public required string RecipeIdentifier { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public required List<CreateRecipeCommandItem> Ingredients { get; set; }
    }

    public record CreateRecipeCommandItem
    {
        public required string Name { get; set; }

        public int Quantity { get; set; }
    }
}