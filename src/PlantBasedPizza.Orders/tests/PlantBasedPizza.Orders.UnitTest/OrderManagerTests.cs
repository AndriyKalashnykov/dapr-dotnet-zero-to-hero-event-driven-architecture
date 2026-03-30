using PlantBasedPizza.OrderManager.Core.Entities;

namespace PlantBasedPizza.Orders.UnitTest;

public class OrderManagerTests
{
    internal const string DefaultCustomerIdentifier = "James";

    [Fact]
    public async Task CanCreateNewOrder_ShouldSetDefaultFields()
    {
        var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

        Assert.NotNull(order.Items);
        Assert.Empty(order.Items);
        Assert.NotNull(order.OrderNumber);
        Assert.NotEmpty(order.OrderNumber);
        Assert.NotNull(order.OrderIdentifier);
        Assert.NotEmpty(order.OrderIdentifier);
        Assert.True(Math.Abs((order.OrderDate - DateTime.Now).TotalSeconds) < 5);
        Assert.Equal(OrderType.Pickup, order.OrderType);
    }

    [Fact]
    public void CanCreateOrderAndAddHistory_ShouldAddHistoryItem()
    {
        var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

        order.AddHistory("Bake complete");

        Assert.Equal(2, order.History().Count);
    }

    [Fact]
    public void CanSetIsAwaitingCollection_ShouldMarkAwaitingAndAddHistory()
    {
        var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

        order.IsAwaitingCollection();

        Assert.Equal(2, order.History().Count);
        Assert.True(order.AwaitingCollection);
    }

    [Fact]
    public void CanCreateNewOrderAndAddItems_ShouldAddToItemArray()
    {
        var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

        var recipeId = "PIZZA1";

        order.AddOrderItem(recipeId, "Pizza 1", 1, 10);
        order.AddOrderItem(recipeId, "Pizza 1", 3, 10);
        order.AddOrderItem("CHIPS", "Chips", 1, 3);

        Assert.Equal(2, order.Items.Count);
        Assert.Equal(4, order.Items.FirstOrDefault(p => p.RecipeIdentifier == recipeId)!.Quantity);
        Assert.Equal(43, order.TotalPrice);
    }

    [Fact]
    public void CanCreateNewOrderAndRemoveItems_ShouldRemove()
    {
        var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

        var recipeId = "PIZZA1";

        order.AddOrderItem(recipeId, "Pizza 1", 1, 10);
        order.AddOrderItem(recipeId, "Pizza 1", 3, 10);
        order.AddOrderItem("CHIPS", "Chips", 1, 3);
        order.AddOrderItem("COCACOLA", "Coca Cola", 2, 1);

        order.RemoveOrderItem(recipeId, 2);
        order.RemoveOrderItem("COCACOLA", 2);

        Assert.Equal(2, order.Items.Count);
        Assert.Equal(2, order.Items.FirstOrDefault(p => p.RecipeIdentifier == recipeId)!.Quantity);
        Assert.Equal(23, order.TotalPrice);
    }

    [Fact]
    public void CanCreateNewDeliveryOrder_ShouldGetDeliveryDetails()
    {
        var order = Order.Create(OrderType.Delivery, DefaultCustomerIdentifier, new DeliveryDetails()
        {
            AddressLine1 = "TEST",
            Postcode = "XN6 7UY"
        });

        Assert.NotNull(order.Items);
        Assert.Empty(order.Items);
        Assert.NotNull(order.OrderNumber);
        Assert.NotEmpty(order.OrderNumber);
        Assert.True(Math.Abs((order.OrderDate - DateTime.Now).TotalSeconds) < 5);
        Assert.Equal(OrderType.Delivery, order.OrderType);
        Assert.Equal("TEST", order.DeliveryDetails!.AddressLine1);
    }

    [Fact]
    public void CanCreateNewDeliveryOrder_ShouldAddDeliveryCharge()
    {
        var order = Order.Create(OrderType.Delivery, DefaultCustomerIdentifier, new DeliveryDetails()
        {
            AddressLine1 = "TEST",
            Postcode = "XN6 7UY"
        });

        order.AddOrderItem("PIZZA", "Pizza 1", 1, 10);

        Assert.Equal(13.50M, order.TotalPrice);
    }

    [Fact]
    public void CanCreateAndSubmitOrder_ShouldBeSubmitted()
    {
        var order = Order.Create(OrderType.Delivery, DefaultCustomerIdentifier, new DeliveryDetails()
        {
            AddressLine1 = "TEST",
            Postcode = "XN6 7UY"
        });

        order.AddOrderItem("PIZZA", "Pizza 1", 1, 10);

        order.SubmitOrder();

        Assert.True(Math.Abs((order.OrderSubmittedOn!.Value - DateTime.Now).TotalSeconds) < 5);
    }

    [Fact]
    public void AddItemsToASubmittedOrder_ShouldNotAdd()
    {
        var order = Order.Create(OrderType.Delivery, DefaultCustomerIdentifier, new DeliveryDetails()
        {
            AddressLine1 = "TEST",
            Postcode = "XN6 7UY"
        });

        order.AddOrderItem("PIZZA", "Pizza 1", 1, 10);

        order.SubmitOrder();

        order.AddOrderItem("PIZZA", "Pizza 1", 1, 10);

        Assert.Equal(1, order.Items.FirstOrDefault()!.Quantity);
    }

    [Fact]
    public void CanCreateAndCompletetOrder_ShouldBeCompleted()
    {
        var order = Order.Create(OrderType.Delivery, DefaultCustomerIdentifier, new DeliveryDetails()
        {
            AddressLine1 = "TEST",
            Postcode = "XN6 7UY"
        });

        order.AddOrderItem("PIZZA", "Pizza 1", 1, 10);

        order.CompleteOrder();

        Assert.True(Math.Abs((order.OrderCompletedOn!.Value - DateTime.Now).TotalSeconds) < 5);
        Assert.False(order.AwaitingCollection);
    }


    [Fact]
    public void SubmitOrderWithNoItems_ShouldError()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var order = Order.Create(OrderType.Pickup, DefaultCustomerIdentifier);

            order.SubmitOrder();
        });
    }


    [Fact]
    public void CanCreateNewOrderWithNoCustomerIdentifier_ShouldError()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Order.Create(OrderType.Pickup, string.Empty);
        });
    }


    [Fact]
    public void CanCreateNewDeliveryOrderWithNoDeliveryDetails_ShouldError()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Order.Create(OrderType.Delivery, DefaultCustomerIdentifier);
        });
    }
}