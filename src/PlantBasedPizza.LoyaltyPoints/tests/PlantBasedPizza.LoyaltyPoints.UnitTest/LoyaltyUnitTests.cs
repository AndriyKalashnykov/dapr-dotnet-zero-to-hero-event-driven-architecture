using FakeItEasy;
using Microsoft.Extensions.Logging;
using PlantBasedPizza.LoyaltyPoints.Shared.Core;

namespace PlantBasedPizza.LoyaltyPoints.UnitTest;

public class LoyaltyUnitTests
{
    [Fact]
    public async Task CanAddLoyaltyPoints_ShouldReturnValidObject()
    {
        var mockRepo = A.Fake<ICustomerLoyaltyPointsRepository>();
        var mockLogger = A.Fake<ILogger<AddLoyaltyPointsCommandHandler>>();

        var customerId = "james";
        CustomerLoyaltyPoints? response = null;

        A.CallTo(() => mockRepo.GetCurrentPointsFor(customerId)).Returns(response);

        var handler = new AddLoyaltyPointsCommandHandler(mockRepo, mockLogger);

        var handleResponse = await handler.Handle(new AddLoyaltyPointsCommand()
        {
            CustomerIdentifier = customerId,
            OrderIdentifier = "order-1",
            OrderValue = 50.79M
        });

        Assert.Equal(51, handleResponse.TotalPoints);
    }

    [Fact]
    public async Task CanAddLoyaltyPointsForExisting_ShouldReturnValidObject()
    {
        var mockRepo = A.Fake<ICustomerLoyaltyPointsRepository>();
        var mockLogger = A.Fake<ILogger<AddLoyaltyPointsCommandHandler>>();
        var customerId = "james";
        CustomerLoyaltyPoints response = new CustomerLoyaltyPoints()
        {
            TotalPoints = 150,
            CustomerId = "james"
        };

        A.CallTo(() => mockRepo.GetCurrentPointsFor(customerId)).Returns(response);

        var handler = new AddLoyaltyPointsCommandHandler(mockRepo, mockLogger);

        var handleResponse = await handler.Handle(new AddLoyaltyPointsCommand()
        {
            CustomerIdentifier = customerId,
            OrderIdentifier = "order-2",
            OrderValue = 50.79M
        });

        Assert.Equal(201, handleResponse.TotalPoints);
    }

    [Fact]
    public async Task CanSpendPoints_ShouldDecreaseFromBalance()
    {
        var mockRepo = A.Fake<ICustomerLoyaltyPointsRepository>();
        var customerId = "james";
        CustomerLoyaltyPoints response = new CustomerLoyaltyPoints()
        {
            TotalPoints = 150,
            CustomerId = "james"
        };

        A.CallTo(() => mockRepo.GetCurrentPointsFor(customerId)).Returns(response);

        var handler = new SpendLoyaltyPointsCommandHandler(mockRepo);

        var handleResponse = await handler.Handle(new SpendLoyaltyPointsCommand()
        {
            CustomerIdentifier = customerId,
            OrderIdentifier = "order-3",
            PointsToSpend = 50
        });

        Assert.Equal(100, handleResponse.TotalPoints);
    }

    [Fact]
    public async Task CanSpendPointsThatAreOver_ShouldError()
    {
        var mockRepo = A.Fake<ICustomerLoyaltyPointsRepository>();
        var customerId = "james";
        CustomerLoyaltyPoints response = new CustomerLoyaltyPoints()
        {
            TotalPoints = 10,
            CustomerId = "james"
        };

        A.CallTo(() => mockRepo.GetCurrentPointsFor(customerId)).Returns(response);

        var handler = new SpendLoyaltyPointsCommandHandler(mockRepo);

        await Assert.ThrowsAsync<InsufficientPointsException>(async () => await handler.Handle(new SpendLoyaltyPointsCommand()
        {
            CustomerIdentifier = customerId,
            OrderIdentifier = "order-4",
            PointsToSpend = 50
        }));
    }
}