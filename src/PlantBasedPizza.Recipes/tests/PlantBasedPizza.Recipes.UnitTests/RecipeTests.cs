using PlantBasedPizza.Recipes.Core.Entities;

namespace PlantBasedPizza.Recipes.UnitTests;

public class RecipeTests
{
    internal const string DefaultRecipeIdentifier = "MyRecipe";

    [Fact]
    public void CanCreateNewOrder_ShouldSetDefaultFields()
    {
        var recipe = new Recipe(RecipeCategory.Pizza, DefaultRecipeIdentifier, "Pizza", 6.5M);

        recipe.AddIngredient("Base", 1);
        recipe.AddIngredient("Tomato Sauce", 1);
        recipe.AddIngredient("Cheese", 1);

        Assert.Equal(DefaultRecipeIdentifier, recipe.RecipeIdentifier);
        Assert.Equal("Pizza", recipe.Name);
        Assert.Equal(6.5M, recipe.Price);
        Assert.Equal(3, recipe.Ingredients.Count);
    }
}