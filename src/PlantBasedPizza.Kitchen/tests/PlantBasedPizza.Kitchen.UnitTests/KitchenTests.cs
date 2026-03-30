using PlantBasedPizza.Kitchen.Core.Adapters;
using PlantBasedPizza.Kitchen.Core.Entities;
using PlantBasedPizza.UnitTest.Builders;

namespace PlantBasedPizza.UnitTest
{
    public class KitchenTests
    {
        internal const string OrderIdentifier = "ORDER123";

        [Fact]
        public void CanCreateNewKitchenRequest_ShouldCreate()
        {
            var request = new KitchenRequestBuilder().AddRecipe("Pizza").Build()!;

            Assert.Single(request.Recipes);
            Assert.Equal(OrderIdentifier, request.OrderIdentifier);
            Assert.Equal(OrderState.NEW, request.OrderState);
            Assert.Null(request.BakeCompleteOn);
            Assert.True(Math.Abs((request.OrderReceivedOn - DateTime.Now).TotalSeconds) < 5);
            Assert.Null(request.PrepCompleteOn);
            Assert.Null(request.QualityCheckCompleteOn);
            Assert.NotNull(request.KitchenRequestId);
        }

        [Fact]
        public void CanCreateWithNullOrderIdentifier_ShouldError()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new KitchenRequest(null!, new List<RecipeAdapter>(1)
                {
                    new RecipeAdapter("Pizza")
                });
            });
        }

        [Fact]
        public void CanCreateAndMarkPreparing_ShouldSetPrepCompleted()
        {
            var request = new KitchenRequestBuilder().AddRecipe("Pizza").Build()!;

            request.Preparing();
            request.PrepComplete();

            Assert.NotNull(request.PrepCompleteOn);
            Assert.True(Math.Abs((request.PrepCompleteOn.Value - DateTime.Now).TotalSeconds) < 5);
        }

        [Fact]
        public void CanCreateAndMarkBaked_ShouldSetBakeComplete()
        {
            var request = new KitchenRequestBuilder().AddRecipe("Pizza").Build()!;

            request.Preparing();
            request.PrepComplete();
            request.BakeComplete();

            Assert.NotNull(request.BakeCompleteOn);
            Assert.True(Math.Abs((request.BakeCompleteOn.Value - DateTime.Now).TotalSeconds) < 5);
        }

        [Fact]
        public async Task CanCreateAndMarkQualityChecked_ShouldSetQualityCheckedOn()
        {
            var request = new KitchenRequestBuilder().AddRecipe("Pizza").Build()!;

            request.Preparing();
            request.PrepComplete();
            request.BakeComplete();
            await request.QualityCheckComplete();

            Assert.NotNull(request.QualityCheckCompleteOn);
            Assert.True(Math.Abs((request.QualityCheckCompleteOn.Value - DateTime.Now).TotalSeconds) < 5);
        }
    }
}